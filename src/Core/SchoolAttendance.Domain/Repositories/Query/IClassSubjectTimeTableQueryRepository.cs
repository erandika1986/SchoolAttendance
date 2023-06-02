using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Repositories.Query
{
    public interface IClassSubjectTimeTableQueryRepository : IQueryRepository<ClassSubjectTimeTable>
    {
        Task<ClassSubjectTimeTable> GetClassSubjectTimeTable(int classId, int subjectId, int dayId, CancellationToken cancellationToken);
    }
}
