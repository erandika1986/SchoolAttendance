using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Repositories.Query
{
    public interface ISubjectAttendanceQueryRepository  : IQueryRepository<SubjectAttendance>
    {
        Task<SubjectAttendance> GetSubjectAttendanceForSelectedDate(int subjectId, int classId, DateTime selectedDate, CancellationToken cancellationToken);
    }
}
