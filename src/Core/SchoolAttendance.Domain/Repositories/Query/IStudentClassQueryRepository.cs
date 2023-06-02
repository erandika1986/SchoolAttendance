using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Repositories.Query
{
    public interface IStudentClassQueryRepository : IQueryRepository<StudentClass>
    {
        Task<List<StudentClass>> GetActiveStudentClassesByClassId(int classId, CancellationToken cancellationToken);
    }
}
