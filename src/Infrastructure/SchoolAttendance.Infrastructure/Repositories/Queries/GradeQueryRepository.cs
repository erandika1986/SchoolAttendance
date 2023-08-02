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
    public class GradeQueryRepository 
        :  QueryRepository<Grade>, IGradeQueryRepository
    {
        public GradeQueryRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }

        public async Task<List<Grade>> GetAllActiveGrades(CancellationToken cancellationToken)
        {
            var grades = await _context.Grades.Where(g => g.IsActive).ToListAsync(cancellationToken);

            return grades;
        }

        public async Task<List<Grade>> GetGradesByLevelHeadId(int levelHeadId, CancellationToken cancellationToken)
        {
            var grades = await _context.Grades.Where(g => g.IsActive && g.LevelHeadId == levelHeadId)
                .ToListAsync(cancellationToken);

            return grades;
        }
    }
}
