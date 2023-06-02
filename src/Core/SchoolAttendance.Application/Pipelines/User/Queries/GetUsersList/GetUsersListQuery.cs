using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.User.Queries.GetUsersList
{
    public record GetUsersListQuery(int roleId, string searchText, int currentPage, int pageSize): IRequest<PaginatedItemsViewModel<UserViewModel>>
    {
    }

    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, PaginatedItemsViewModel<UserViewModel>>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public GetUsersListQueryHandler(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }

        public async Task<PaginatedItemsViewModel<UserViewModel>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var vms = new List<UserViewModel>();

            var query = ( await _userQueryRepository
                .Query(r => r.UserRoles.Any(u => u.RoleId == request.roleId) && r.IsActive == true))
                .OrderBy(x => x.FullName);

            if (!string.IsNullOrEmpty(request.searchText))
            {
                query = query.Where(x => x.FullName.Contains(request.searchText)).OrderBy(u => u.FullName);
            }

            var totalRecordCount = query.Count();
            var totalPages = (double)totalRecordCount / request.pageSize;
            var totalPageCount = (int)Math.Ceiling(totalPages);

            var userList = await query
                .Skip((request.currentPage - 1) * request.pageSize)
                .Take(request.pageSize)
                .ToListAsync();

            userList.ForEach(u =>
            {
                vms.Add(u.ToVm());

            });

            var container = new PaginatedItemsViewModel<UserViewModel>
                (
                    request.currentPage, 
                    request.pageSize, 
                    totalPageCount, 
                    totalRecordCount, 
                    vms
                  );

            return container;
        }
    }
}
