using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllAcademicLevels;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetTeachGradesForLoggedInUser
{
    public record GetTeachGradesForLoggedInUserQuery(int academicYear): IRequest<List<DropDownViewModel>>
    {
    }

    public class GetTeachGradesForLoggedInUserQueryHandler : IRequestHandler<GetTeachGradesForLoggedInUserQuery, List<DropDownViewModel>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IGradeQueryRepository _gradeQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IClassSubjectQueryRepository _classSubjectQueryRepository;
        private readonly IMediator _mediator;

        public GetTeachGradesForLoggedInUserQueryHandler(
            IUserQueryRepository userQueryRepository, 
            IGradeQueryRepository gradeQueryRepository, 
            ICurrentUserService currentUserService, 
            IClassSubjectQueryRepository classSubjectQueryRepository,
            IMediator mediator)
        {
            this._userQueryRepository = userQueryRepository;
            this._gradeQueryRepository = gradeQueryRepository;
            this._currentUserService = currentUserService;
            this._classSubjectQueryRepository = classSubjectQueryRepository;
            this._mediator = mediator;
        }
        public async Task<List<DropDownViewModel>> Handle(GetTeachGradesForLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            var response = new List<DropDownViewModel>();

            var loggedInUser = await _userQueryRepository
                .GetById(_currentUserService.UserId.Value, cancellationToken);

            var roles = loggedInUser
             .UserRoles
             .Select(x => x.RoleId)
             .ToList();

            if (roles.Any(x => x == (int)SystemRole.Admin) ||
                roles.Any(x => x == (int)SystemRole.Principle) ||
                roles.Any(x => x == (int)SystemRole.VicePrinciple) ||
                roles.Any(x => x == (int)SystemRole.DepartmentHead))
            {
                var rolesList = await _mediator.Send(new GetAllAcademicLevelsQuery());

                response.AddRange(rolesList);
            }
            else if (roles.Any(x => x == (int)SystemRole.LevelHead))
            {
                var assignedGrades = await _gradeQueryRepository
                    .GetGradesByLevelHeadId(loggedInUser.Id, cancellationToken);

                var assignGradeDropDows = assignedGrades
                    .Select(g =>
                        new DropDownViewModel()
                        {
                            Id = g.Id,
                            Name = g.Name
                        })
                    .ToList();

                var assignedClassSubject = await _classSubjectQueryRepository
                    .GetClassForSelectedSubjectTeacher(request.academicYear, loggedInUser.Id, cancellationToken);

                var grades = assignedClassSubject
                    .Where(x =>
                                !assignGradeDropDows
                                .Any(g => g.Id == x.Class.GradeId))
                  .Select(a => a.Class.Grade)
                  .Distinct()
                  .Select(g =>
                    new DropDownViewModel()
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                  .ToList()
                  .Union(assignGradeDropDows)
                  .ToList();

                response.AddRange(grades);
            }
            else if (roles.Any(x => x == (int)SystemRole.Teacher))
            {
                var assignedClassSubject = await _classSubjectQueryRepository
                    .GetClassForSelectedSubjectTeacher(request.academicYear, loggedInUser.Id, cancellationToken);

                var grades = assignedClassSubject
                    .Select(a => a.Class.Grade)
                    .Distinct()
                    .Select(g =>
                        new DropDownViewModel()
                        {
                            Id = g.Id,
                            Name = g.Name
                        })
                    .ToList();

                response.AddRange(grades);
            }

            return response;
        }
    }
}
