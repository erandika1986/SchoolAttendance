﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Repositories.Query
{
    public interface IAcademicYearQueryRepository : IQueryRepository<AcademicYear>
    {
        Task<AcademicYear> GetCurrentAcademicYear(CancellationToken cancellationToken);
    }
}
