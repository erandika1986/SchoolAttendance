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

        public Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
