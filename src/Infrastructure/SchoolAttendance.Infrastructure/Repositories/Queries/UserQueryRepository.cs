using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Query;
using SchoolAttendance.Infrastructure.Data;
using SchoolAttendance.Infrastructure.Repositories.Queries.Base;

namespace SchoolAttendance.Infrastructure.Repositories.Queries
{
    public class UserQueryRepository : QueryRepository<User>, IUserQueryRepository
    {
        public UserQueryRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }

        public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == email.Trim().ToLower());

            return user;
        }

        public async Task<User> GetUserByUsername(string userName, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Trim().ToLower() == userName.Trim().ToLower());

            return user;
        }
    }
}
