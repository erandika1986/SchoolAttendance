using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAssignedClassSubjectForLoggedInUser
{
    public record GetAssignedClassSubjectForLoggedInUserQuery(int classId) : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetAssignedClassSubjectForLoggedInUserQueryHandler : IRequestHandler<GetAssignedClassSubjectForLoggedInUserQuery, List<DropDownViewModel>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IClassQueryRepository _classQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IClassSubjectQueryRepository _classSubjectQueryRepository;

        public GetAssignedClassSubjectForLoggedInUserQueryHandler(
            IUserQueryRepository userQueryRepository, 
            ICurrentUserService currentUserService, 
            IClassQueryRepository classQueryRepository,
            IClassSubjectQueryRepository classSubjectQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
            this._currentUserService = currentUserService;
            this._classQueryRepository = classQueryRepository;
            this._classSubjectQueryRepository = classSubjectQueryRepository;

        }

        public async Task<List<DropDownViewModel>> Handle(GetAssignedClassSubjectForLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            var loggedInUser = await _userQueryRepository
                .GetById(_currentUserService.UserId.Value, cancellationToken);

            var response = new List<DropDownViewModel>();
            if (request.classId > 0)
            {
                var roles = loggedInUser
                    .UserRoles
                .Select(x => x.RoleId)
                    .ToList();

                var classObj = await _classQueryRepository.GetById(request.classId, cancellationToken);

                var matchingLevelHead = loggedInUser
                    .Grades
                    .FirstOrDefault(x => x.Id == classObj.GradeId);

                var subjects = (await _classSubjectQueryRepository
                    .Query(x => x.ClassId == request.classId)).ToList();

                if (roles.Contains((int)SystemRole.Admin) ||
                    roles.Contains((int)SystemRole.Principle) ||
                    roles.Contains((int)SystemRole.VicePrinciple) ||
                    matchingLevelHead != null ||
                    classObj.ClassTeacherId == loggedInUser.Id
                    )
                {

                }
                else if (
                    (roles.Contains((int)SystemRole.DepartmentHead) ||
                    roles.Contains((int)SystemRole.Teacher)))
                {
                    subjects = subjects
                        .Where(x => x.SubjectTeacherId == loggedInUser.Id)
                        .ToList();
                }

                var assignedSubject = subjects.Distinct()
                  .Select(s => new DropDownViewModel()
                  {
                      Id = s.Subject.Id,
                      Name = s.Subject.Name
                  })
                  .OrderBy(x => x.Name)
                  .ToList();

                response.AddRange(assignedSubject);
            }

            return response;
        }
    }
}
