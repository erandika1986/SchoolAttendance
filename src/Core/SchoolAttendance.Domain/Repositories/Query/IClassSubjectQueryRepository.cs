using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Repositories.Query
{
    public interface IClassSubjectQueryRepository : IQueryRepository<ClassSubject>
    {
        Task<List<ClassSubject>> GetClassForSelectedSubjectTeacher(int academicYear, int subjectTeacher, CancellationToken cancellationToken);
    }
}
