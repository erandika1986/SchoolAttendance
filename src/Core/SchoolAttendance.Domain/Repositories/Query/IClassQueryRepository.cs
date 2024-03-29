﻿using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Repositories.Query
{
    public interface IClassQueryRepository : IQueryRepository<Class>
    {
        Task<List<Class>> GetClassesForSelectedGrade(int academicYearId, int gradeId, CancellationToken cancellationToken);
    }
}
