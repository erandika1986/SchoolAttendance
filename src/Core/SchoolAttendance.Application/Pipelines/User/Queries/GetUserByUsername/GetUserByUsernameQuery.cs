using MediatR;
using SchoolAttendance.Application.Pipelines.Attendance.Queries.GetAttendanceDetailForSubjectClassById;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.User.Queries.GetUserByUsername
{
    public record GetUserByUsernameQuery(string username) : IRequest<SchoolAttendance.Domain.Entities.User>
    {
    }

    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, SchoolAttendance.Domain.Entities.User>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public GetUserByUsernameQueryHandler(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }
        public async Task<Domain.Entities.User> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userQueryRepository.GetUserByUsername(request.username, cancellationToken);

            return user;
        }
    }
}
