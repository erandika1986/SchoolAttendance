using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAssignedClassForLoggedInUser
{
    public record GetAssignedClassForLoggedInUserQuery(int academicYear, int gradeId) : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetAssignedClassForLoggedInUserQueryHandler : IRequestHandler<GetAssignedClassForLoggedInUserQuery, List<DropDownViewModel>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IGradeQueryRepository _gradeQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IClassSubjectQueryRepository _classSubjectQueryRepository;
        private readonly IMediator _mediator;

        public GetAssignedClassForLoggedInUserQueryHandler(
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


        public async Task<List<DropDownViewModel>> Handle(GetAssignedClassForLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            var response = new List<DropDownViewModel>();

            var loggedInUser = await _userQueryRepository
                .GetById(_currentUserService.UserId.Value, cancellationToken);

            var roles = loggedInUser
             .UserRoles
             .Select(x => x.RoleId)
             .ToList();

            var selectedGrade = await _gradeQueryRepository
                .GetById(request.gradeId, cancellationToken);

            var allClasses = selectedGrade
                .Classes
                .Where(x => x.AcademicYear == request.academicYear)
                .ToList();

            if (!roles.Contains((int)SystemRole.Teacher))
            {

                var classes = allClasses
                  .Distinct()
                  .Select(d => new DropDownViewModel() { Id = d.Id, Name = d.Name })
                  .OrderBy(x => x.Name)
                  .ToList();

                response.AddRange(classes);

            }
            else if (roles.Contains((int)SystemRole.Teacher))
            {

                var classTeachers = allClasses
                    .Where(t => t.ClassTeacherId == loggedInUser.Id)
                    .Select(d =>
                        new DropDownViewModel()
                        {
                            Id = d.Id,
                            Name = d.Name
                        })
                    .ToList();

                var subjectTeacherClasses = allClasses
                    .Where(x =>
                                x.ClassSubjects.Any(x => x.SubjectTeacherId == loggedInUser.Id))
                                .Where(cl => !classTeachers.Any(t => t.Id == cl.Id))
                    .Select(d =>
                            new DropDownViewModel()
                            {
                                Id = d.Id,
                                Name = d.Name
                            })
                    .ToList()
                    .Union(classTeachers)
                    .Distinct()
                    .ToList();

                response.AddRange(subjectTeacherClasses);
            }

            return response;
        }
    }
}
