using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllSystemRoles
{
    public class GetAllSystemRolesQuery : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetAllSystemRolesQueryHandler : IRequestHandler<GetAllSystemRolesQuery, List<DropDownViewModel>>
    {

        public async Task<List<DropDownViewModel>> Handle(GetAllSystemRolesQuery request, CancellationToken cancellationToken)
        {
            var response = new List<DropDownViewModel>();

            foreach (SystemRole role in (SystemRole[])Enum.GetValues(typeof(SystemRole)))
            {
                if (role != SystemRole.Parent && role != SystemRole.Student)
                {
                    response.Add(new DropDownViewModel() { Id = (int)role, Name = EnumHelper.GetEnumDescription(role) });
                }
            }

            return response;
        }
    }
}
