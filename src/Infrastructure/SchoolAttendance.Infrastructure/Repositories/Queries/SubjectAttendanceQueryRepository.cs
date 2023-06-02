using Microsoft.EntityFrameworkCore;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Query;
using SchoolAttendance.Infrastructure.Data;
using SchoolAttendance.Infrastructure.Repositories.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Repositories.Queries
{
    public class SubjectAttendanceQueryRepository
        : QueryRepository<SubjectAttendance>, ISubjectAttendanceQueryRepository
    {
        public SubjectAttendanceQueryRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }

        public async Task<SubjectAttendance> GetSubjectAttendanceForSelectedDate(int subjectId, int classId, DateTime selectedDate, CancellationToken cancellationToken)
        {
            var subjectAttendance = await _context.SubjectAttendances
                .FirstOrDefaultAsync(x => 
                    x.SubjectId == subjectId && 
                    x.ClassId == classId && 
                    x.Date == selectedDate, cancellationToken);

            return subjectAttendance;
        }
    }
}
