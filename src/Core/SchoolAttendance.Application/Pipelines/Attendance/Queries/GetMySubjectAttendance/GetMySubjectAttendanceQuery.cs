using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.Attendance.Queries.GetMySubjectAttendance
{
    public record GetMySubjectAttendanceQuery(
        string searchText, 
        int currentPage, 
        int pageSize, 
        int academicYearId, 
        int gradeId, 
        int classId, 
        int subjectId) : IRequest<PaginatedItemsViewModel<BasicAttendanceViewModel>>
    {
    }

    public class GetMySubjectAttendanceQueryHandler : IRequestHandler<GetMySubjectAttendanceQuery, PaginatedItemsViewModel<BasicAttendanceViewModel>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISubjectAttendanceQueryRepository _subjectAttendanceQueryRepository;
        private readonly IClassSubjectQueryRepository _classSubjectQueryRepository;

        public GetMySubjectAttendanceQueryHandler(
            IUserQueryRepository userQueryRepository, 
            ICurrentUserService currentUserService, 
            ISubjectAttendanceQueryRepository subjectAttendanceQueryRepository, 
            IClassSubjectQueryRepository classSubjectQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
            this._currentUserService = currentUserService;
            this._subjectAttendanceQueryRepository = subjectAttendanceQueryRepository;
            this._classSubjectQueryRepository = classSubjectQueryRepository;

        }

        public async Task<PaginatedItemsViewModel<BasicAttendanceViewModel>> Handle(GetMySubjectAttendanceQuery request, CancellationToken cancellationToken)
        {
            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var vms = new List<BasicAttendanceViewModel>();

            var loggedInUser = await _userQueryRepository
                .GetById(_currentUserService.UserId.Value, cancellationToken);

            var roles = loggedInUser
                .UserRoles
                .Select(x => x.RoleId).ToList();

            var subjectAttendances = (await 
                _subjectAttendanceQueryRepository
                .GetAll(cancellationToken)
                ).OrderByDescending(x => x.Date);

            if (roles.Contains((int)SystemRole.Admin) || 
                roles.Contains((int)SystemRole.Principle) || 
                roles.Contains((int)SystemRole.VicePrinciple))
            {

            }
            else if (roles.Contains((int)SystemRole.LevelHead))
            {
                subjectAttendances = subjectAttendances.Where(x => x.Class.GradeId == request.gradeId).OrderByDescending(x => x.Date);
            }
            else if (roles.Contains((int)SystemRole.DepartmentHead))
            {
                subjectAttendances = subjectAttendances.Where(x => x.SubjectId == request.subjectId).OrderByDescending(x => x.Date);
            }
            else if (roles.Contains((int)SystemRole.Teacher))
            {
                var classSubjects = (
                    await _classSubjectQueryRepository
                    .Query(x => x.SubjectTeacherId == loggedInUser.Id))
                    .ToList();

                //subjectAttendances = subjectAttendances.Where(x=> mySubjects.Any(y=>y.SubjectId==x.SubjectId && y.ClassId==x.ClassId)).OrderByDescending(x => x.Date);

                subjectAttendances = (from st in subjectAttendances join cs in classSubjects on new { st.ClassId, st.SubjectId } equals new { cs.ClassId, cs.SubjectId } select st).OrderByDescending(x => x.Date);
            }


            if (!string.IsNullOrEmpty(request.searchText))
            {
                subjectAttendances = subjectAttendances.Where(x => x.Class.Name.Contains(request.searchText))
                  .OrderByDescending(x => x.Date);
            }

            if (request.academicYearId > 0)
            {
                subjectAttendances = subjectAttendances.Where(x => x.Class.AcademicYear == request.academicYearId)
                  .OrderByDescending(x => x.Date);
            }

            if (request.gradeId > 0)
            {
                subjectAttendances = subjectAttendances.Where(x => x.Class.GradeId == request.gradeId)
                  .OrderByDescending(x => x.Date);
            }

            if (request.classId > 0)
            {
                subjectAttendances = subjectAttendances.Where(x => x.ClassId == request.classId)
                  .OrderByDescending(x => x.Date);
            }

            if (request.subjectId > 0)
            {
                subjectAttendances = subjectAttendances.Where(x => x.SubjectId == request.subjectId)
                  .OrderByDescending(x => x.Date);
            }

            totalRecordCount = subjectAttendances.Count();
            totalPages = (double)totalRecordCount / request.pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var attendanceList = subjectAttendances.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();

            attendanceList.ForEach(a =>
            {
                vms.Add(new BasicAttendanceViewModel()
                {
                    Id = a.Id,
                    ClassName = a.Class.Name,
                    SubjectName = a.Subject.Name,
                    SubjectTeacherName = a.Class.ClassSubjects.FirstOrDefault(x => x.SubjectId == a.SubjectId).SubjectTeacher.FullName,
                    TotalAttendedStudents = a.StudentSubjectAttendances.Where(x => x.IsAttended).Count(),
                    TotalAbsenceStudents = a.StudentSubjectAttendances.Where(x => x.IsAttended == false).Count(),
                    Date = a.Date.ToString("MMM/dd/yyyy"),
                    EndTime = a.EndTime.ToString("hh:mm tt"),
                    StartTime = a.StartTime.ToString("hh:mm tt")
                });

            });

            var container = new PaginatedItemsViewModel<BasicAttendanceViewModel>(request.currentPage, request.pageSize, totalPageCount, totalRecordCount, vms);

            return container;
        }
    }
}
