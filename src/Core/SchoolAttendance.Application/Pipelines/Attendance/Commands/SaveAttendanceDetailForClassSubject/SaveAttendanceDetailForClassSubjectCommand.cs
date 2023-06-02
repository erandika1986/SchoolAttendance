using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.Attendance.Commands.SaveAttendanceDetailForClassSubject
{
    public class SaveAttendanceDetailForClassSubjectCommand : IRequest<ResponseViewModel>
    {
        public SubjectAttendaceViewModel SubjectAttendaceViewModel { get; set; }
    }

    public class SaveAttendanceDetailForClassSubjectCommandHandler : IRequestHandler<SaveAttendanceDetailForClassSubjectCommand, ResponseViewModel>
    {
        private readonly ISubjectAttendanceQueryRepository _subjectAttendanceQueryRepository;
        private readonly ISubjectAttendanceCommandRepository _subjectAttendanceCommandRepository;
        private readonly IStudentSubjectAttendanceCommandRepository _studentSubjectAttendanceCommandRepository;
        private readonly ILogger<SaveAttendanceDetailForClassSubjectCommand> _logger;

        public SaveAttendanceDetailForClassSubjectCommandHandler(
            ISubjectAttendanceCommandRepository subjectAttendanceCommandRepository,
            ISubjectAttendanceQueryRepository subjectAttendanceQueryRepository,
            IStudentSubjectAttendanceCommandRepository studentSubjectAttendanceCommandRepository,
            ILogger<SaveAttendanceDetailForClassSubjectCommand> logger)
        {
            this._subjectAttendanceQueryRepository = subjectAttendanceQueryRepository;
            this._subjectAttendanceCommandRepository = subjectAttendanceCommandRepository;
            this._studentSubjectAttendanceCommandRepository = studentSubjectAttendanceCommandRepository;
            this._logger = logger;

        }

        public async Task<ResponseViewModel> Handle(SaveAttendanceDetailForClassSubjectCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var subjectAttendance = await _subjectAttendanceQueryRepository.GetById(request.SubjectAttendaceViewModel.Id, cancellationToken);

                if (subjectAttendance == null)
                {
                    subjectAttendance = new SubjectAttendance()
                    {
                        ClassId = request.SubjectAttendaceViewModel.ClassId,
                        Date = new DateTime(request.SubjectAttendaceViewModel.Year, request.SubjectAttendaceViewModel.Month, request.SubjectAttendaceViewModel.Day, 0, 0, 0),
                        EndTime = new DateTime(request.SubjectAttendaceViewModel.Year, request.SubjectAttendaceViewModel.Month, request.SubjectAttendaceViewModel.Day, request.SubjectAttendaceViewModel.EndHour, request.SubjectAttendaceViewModel.EndMin, 0),
                        StartTime = new DateTime(request.SubjectAttendaceViewModel.Year, request.SubjectAttendaceViewModel.Month, request.SubjectAttendaceViewModel.Day, request.SubjectAttendaceViewModel.StartHour, request.SubjectAttendaceViewModel.StartMin, 0),
                        SubjectId = request.SubjectAttendaceViewModel.SubjectId,
                        IsExtraClass = request.SubjectAttendaceViewModel.IsExtraClass,
                        LessonDetails = request.SubjectAttendaceViewModel.LessonDetails,
                        IsReScheduleClass = false,
                        UsedSoftwareName = request.SubjectAttendaceViewModel.SoftwareName,
                        ActualEnteredDate = DateTime.UtcNow
                    };

                    if (request.SubjectAttendaceViewModel.TimeSlotId > 0)
                    {
                        subjectAttendance.TimeSlotId = request.SubjectAttendaceViewModel.TimeSlotId;
                    }

                    subjectAttendance.StudentSubjectAttendances = new HashSet<StudentSubjectAttendance>();

                    request.SubjectAttendaceViewModel.StudentsAttendance.ForEach(st =>
                    {
                        subjectAttendance.StudentSubjectAttendances.Add(new StudentSubjectAttendance()
                        {
                            IsAttended = st.IsPresent,
                            StudentId = st.StudentId,
                        });
                    });

                    await _subjectAttendanceCommandRepository.AddAsync(subjectAttendance, cancellationToken);
                    response.Message = "Attendance details added successfully.";
                }
                else
                {
                    subjectAttendance.Date = new DateTime(request.SubjectAttendaceViewModel.Year, request.SubjectAttendaceViewModel.Month, request.SubjectAttendaceViewModel.Day, 0, 0, 0);
                    subjectAttendance.EndTime = new DateTime(request.SubjectAttendaceViewModel.Year, request.SubjectAttendaceViewModel.Month, request.SubjectAttendaceViewModel.Day, request.SubjectAttendaceViewModel.EndHour, request.SubjectAttendaceViewModel.EndMin, 0);
                    subjectAttendance.StartTime = new DateTime(request.SubjectAttendaceViewModel.Year, request.SubjectAttendaceViewModel.Month, request.SubjectAttendaceViewModel.Day, request.SubjectAttendaceViewModel.StartHour, request.SubjectAttendaceViewModel.StartMin, 0);
                    subjectAttendance.LessonDetails = request.SubjectAttendaceViewModel.LessonDetails;
                    subjectAttendance.IsExtraClass = request.SubjectAttendaceViewModel.IsExtraClass;
                    subjectAttendance.UsedSoftwareName = request.SubjectAttendaceViewModel.SoftwareName;

                    await _subjectAttendanceCommandRepository.UpdateAsync(subjectAttendance, cancellationToken);

                    foreach (var item in request.SubjectAttendaceViewModel.StudentsAttendance)
                    {
                        var student = subjectAttendance.StudentSubjectAttendances.FirstOrDefault(x => x.StudentId == item.StudentId);
                        student.IsAttended = item.IsPresent;

                        await _studentSubjectAttendanceCommandRepository.UpdateAsync(student, cancellationToken);
                    }

                    response.Message = "Attendance details updated successfully.";
                }

                response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "An Exception has been occuured while saving the record. Please try again.";
            }

            return response;
        }
    }
}
