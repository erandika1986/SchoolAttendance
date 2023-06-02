using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Helpers;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.Attendance.Queries.GetAttendanceDetailForClassSubject
{
    public record GetAttendanceDetailForClassSubjectQuery(AttendanceFilterViewModel filter): IRequest<SubjectAttendaceViewModel>
    {
    }

    public class GetAttendanceDetailForClassSubjectQueryHandler : IRequestHandler<GetAttendanceDetailForClassSubjectQuery, SubjectAttendaceViewModel>
    {
        private readonly ISubjectAttendanceQueryRepository _subjectAttendanceQueryRepository;
        private readonly IClassSubjectTimeTableQueryRepository _classSubjectTimeTableQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IStudentClassQueryRepository _studentClassQueryRepository;
        private ICurrentUserService _currentUserService;
        private readonly IAttendanceService _attendanceService;

        public GetAttendanceDetailForClassSubjectQueryHandler(
            ISubjectAttendanceQueryRepository subjectAttendanceQueryRepository,
            IClassSubjectTimeTableQueryRepository classSubjectTimeTableQueryRepository,
            IUserQueryRepository userQueryRepository, 
            IStudentClassQueryRepository studentClassQueryRepository, 
            ICurrentUserService currentUserService, IAttendanceService attendanceService)
        {
            this._subjectAttendanceQueryRepository = subjectAttendanceQueryRepository;
            this._classSubjectTimeTableQueryRepository = classSubjectTimeTableQueryRepository;
            this._userQueryRepository = userQueryRepository;
            this._studentClassQueryRepository = studentClassQueryRepository;
            this._currentUserService = currentUserService;
            this._attendanceService = attendanceService;
        }

        public async Task<SubjectAttendaceViewModel> Handle(GetAttendanceDetailForClassSubjectQuery request, CancellationToken cancellationToken)
        {
            var response = new SubjectAttendaceViewModel();

            var date = new DateTime(request.filter.Year, request.filter.Month, request.filter.Day, 0, 0, 0);

            var subjectAttendance = await _subjectAttendanceQueryRepository
                .GetSubjectAttendanceForSelectedDate(request.filter.SubjectId, request.filter.ClassId, date, cancellationToken);

            if (subjectAttendance == null)
            {
                var timeTableRecord = await _classSubjectTimeTableQueryRepository
                    .GetClassSubjectTimeTable(request.filter.ClassId, request.filter.SubjectId, (int)date.DayOfWeek, cancellationToken);

                var currentUser = await _userQueryRepository
                    .GetById(_currentUserService.UserId.Value, cancellationToken);

                var currentLocalTime = DateTimeHelper.ConvertToLocalTime(DateTime.UtcNow, currentUser.TimeZoneId);

                var endLocalTime = currentLocalTime.AddMinutes(90);

                response.ClassId = request.filter.ClassId;
                response.GradeId = request.filter.GradeId;
                response.Day = request.filter.Day;
                response.Month = request.filter.Month;
                response.StartHour = timeTableRecord == null ? currentLocalTime.Hour : timeTableRecord.StartTime.Hour;
                response.StartMin = timeTableRecord == null ? currentLocalTime.Minute : timeTableRecord.StartTime.Minute;
                response.EndHour = timeTableRecord == null ? endLocalTime.Hour : timeTableRecord.EndTime.Hour;
                response.EndMin = timeTableRecord == null ? endLocalTime.Minute : timeTableRecord.EndTime.Minute;
                response.SubjectId = request.filter.SubjectId;
                response.Year = request.filter.Year;

                if (timeTableRecord != null)
                {
                    response.TimeSlotId = timeTableRecord.Id;
                }
                var studentClass = await _studentClassQueryRepository
                    .GetActiveStudentClassesByClassId(request.filter.ClassId, cancellationToken);

                foreach (var item in studentClass)
                {
                    var student = new StudentAttendanceViewModel()
                    {
                        IsPresent = false,
                        StudentId = item.StudentId,
                        IndexNo = item.Student.Username,
                        Gender = item.Student.Gender,
                        ImagePath = item.Student.Gender == "M" ? "assets/images/student-m.png" : "assets/images/student-f.png",
                        StudentName = item.Student.FullName
                    };

                    response.StudentsAttendance.Add(student);
                }
            }
            else
            {
                response = _attendanceService
                    .GetSubjectAttendaceViewModel(subjectAttendance);
            }

            response.StudentsAttendance = response
                .StudentsAttendance
                .OrderBy(x => x.StudentName)
                .ToList();

            return response;
        }
    }
}
