using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.DropDown.Queries.GetAllLevelHeads
{
    public class GetAllLevelHeadsQuery : IRequest<List<DropDownViewModel>>
    {
    }

    public class GetAllLevelHeadsQueryHandler : IRequestHandler<GetAllLevelHeadsQuery, List<DropDownViewModel>>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public GetAllLevelHeadsQueryHandler(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }


        public async Task<List<DropDownViewModel>> Handle(GetAllLevelHeadsQuery request, CancellationToken cancellationToken)
        {
            var levelHeadId = (int)SystemRole.LevelHead;

            var allTeachers = (
                                await _userQueryRepository
                                .Query(x => x.IsActive == true && x.UserRoles.Any(r => r.RoleId == levelHeadId)))
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
