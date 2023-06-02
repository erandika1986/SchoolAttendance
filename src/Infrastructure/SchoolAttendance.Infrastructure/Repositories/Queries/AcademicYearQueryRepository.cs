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
    public class AcademicYearQueryRepository 
        : QueryRepository<AcademicYear>, IAcademicYearQueryRepository
    {
        public AcademicYearQueryRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }

        public async Task<AcademicYear> GetCurrentAcademicYear(CancellationToken cancellationToken)
        {
            var currentAcademicYear = await _context.AcademicYears.FirstOrDefaultAsync(x => x.IsCurrentYear == true,cancellationToken);

            return currentAcademicYear;
        }
    }
}
