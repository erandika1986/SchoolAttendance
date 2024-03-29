﻿using Microsoft.EntityFrameworkCore;
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
    public class ClassQueryRepository 
        : QueryRepository<Class>, IClassQueryRepository
    {
        public ClassQueryRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }

        public async Task<List<Class>> GetClassesForSelectedGrade(int academicYearId, int gradeId, CancellationToken cancellationToken)
        {
            var classes = await _context
                .Classes
                .Where(x => x.GradeId == gradeId && x.AcademicYear == academicYearId && x.Grade.IsActive == true)
                .ToListAsync(cancellationToken); 

            return classes;
        }
    }
}
