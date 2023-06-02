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
    public class ClassSubjectTimeTableQueryRepository  : 
        QueryRepository<ClassSubjectTimeTable>, IClassSubjectTimeTableQueryRepository
    {
        public ClassSubjectTimeTableQueryRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }

        public async Task<ClassSubjectTimeTable> GetClassSubjectTimeTable(int classId, int subjectId, int dayId, CancellationToken cancellationToken)
        {
            var classSubjectTimeTable = await _context.ClassSubjectTimeTables
                .FirstOrDefaultAsync(x => 
                    x.ClassId == classId && 
                    x.SubjectId == subjectId && 
                    x.DayId == dayId, cancellationToken);

            return classSubjectTimeTable;
        }
    }
}
