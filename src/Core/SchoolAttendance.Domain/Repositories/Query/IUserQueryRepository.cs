using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Repositories.Query
{
    public interface IUserQueryRepository : IQueryRepository<User>
    {
        Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);
    }
}
