using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllTeachers
{
    public class GetAllTeachersQuery : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, List<DropDownViewModel>>
    {
        IUserQueryRepository _userQueryRepository;

        public GetAllTeachersQueryHandler(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }

        public async Task<List<DropDownViewModel>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var teacherId = (int)SystemRole.Teacher;

            var allTeachers =  (
                    await _userQueryRepository
                    .Query(x => x.IsActive == true && x.UserRoles.Any(r => r.RoleId == teacherId)))
                    .OrderBy(x => x.FullName)
                    .Select(u => 
                        new DropDownViewModel() 
                        { 
                            Id = u.Id, 
                            Name = u.FullName 
                        }).ToList();

            return allTeachers;
        }
    }
}
