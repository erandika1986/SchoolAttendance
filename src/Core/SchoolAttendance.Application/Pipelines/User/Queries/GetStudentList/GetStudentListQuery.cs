using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.User.Queries.GetStudentList
{
    public record GetStudentListQuery(string searchText, int currentPage, int pageSize, int academicYearId, int gradeId, int classId) : IRequest<PaginatedItemsViewModel<BasicStudentViewModel>>
    {
    }

    public class GetStudentQueryHandler : IRequestHandler<GetStudentListQuery, PaginatedItemsViewModel<BasicStudentViewModel>>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public GetStudentQueryHandler(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }

        public async Task<PaginatedItemsViewModel<BasicStudentViewModel>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var vms = new List<BasicStudentViewModel>();

            var users = (await _userQueryRepository
                .Query(x => x.UserRoles.Any(x => x.RoleId == (int)SystemRole.Student) && x.IsActive == true))
                .OrderBy(u => u.FullName);

            if (request.academicYearId > 0)
            {
                users = users
                    .Where(x => x.StudentClasses.FirstOrDefault(x => x.IsActive == true).Class.AcademicYear == request.academicYearId)
                    .OrderBy(u => u.FullName);
            }

            if (request.gradeId > 0)
            {
                users = users
                    .Where(x => x.StudentClasses.FirstOrDefault(x => x.IsActive == true).Class.GradeId == request.gradeId)
                    .OrderBy(u => u.FullName);
            }

            if (request.classId > 0)
            {
                users = users
                    .Where(x => x.StudentClasses.FirstOrDefault(x => x.IsActive == true).ClassId == request.classId)
                    .OrderBy(u => u.FullName);
            }

            if (!string.IsNullOrEmpty(request.searchText))
            {
                users = users
                    .Where(x => x.FullName.Contains(request.searchText))
                    .OrderBy(u => u.FullName);
            }



            var totalRecordCount = users.Count();
            var totalPages = (double)totalRecordCount / request.pageSize;
            var totalPageCount = (int)Math.Ceiling(totalPages);

            var userList = users.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();

            userList.ForEach(u =>
            {
                var studentClass = u.StudentClasses.FirstOrDefault(x => x.IsActive == true);

                vms.Add(new BasicStudentViewModel()
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    IndexNo = u.Username,
                    TimeZoneId = u.TimeZoneId,
                    Gender = u.Gender == "F" ? "Female" : "Male",
                    Year = studentClass == null ? "Not Assigned" : studentClass.Class.AcademicYearNavigation.Name,
                    ClassName = studentClass == null ? "Not Assigned" : studentClass.Class.Name,
                    Grade = studentClass == null ? "Not Assigned" : studentClass.Class.Grade.Name
                });

            });

            var container = new PaginatedItemsViewModel<BasicStudentViewModel>(request.currentPage, request.pageSize, totalPageCount, totalRecordCount, vms);

            return container;
        }
    }
}
