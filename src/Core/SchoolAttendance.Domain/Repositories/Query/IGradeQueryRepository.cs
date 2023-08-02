using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Repositories.Query
{
    public interface IGradeQueryRepository : IQueryRepository<Grade>
    {
        Task<List<Grade>> GetAllActiveGrades(CancellationToken cancellationToken);
        Task<List<Grade>> GetGradesByLevelHeadId(int levelHeadId, CancellationToken cancellationToken);
    }
}
