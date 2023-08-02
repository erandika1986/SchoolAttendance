using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllDepartmentHeads
{
    public class GetAllDepartmentHeadsQuery : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetAllDepartmentHeadsQueryHandler : IRequestHandler<GetAllDepartmentHeadsQuery, List<DropDownViewModel>>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public GetAllDepartmentHeadsQueryHandler(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }

        public async Task<List<DropDownViewModel>> Handle(GetAllDepartmentHeadsQuery request, CancellationToken cancellationToken)
        {
            var departmentHeadId = (int)SystemRole.DepartmentHead;
            var allTeachers = (await _userQueryRepository
                .Query(x => x.IsActive == true && x.UserRoles.Any(r => r.RoleId == departmentHeadId)))
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
