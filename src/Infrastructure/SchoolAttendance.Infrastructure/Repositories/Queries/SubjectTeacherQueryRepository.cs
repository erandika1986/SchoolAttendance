﻿using SchoolAttendance.Domain.Entities;
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
    public class SubjectTeacherQueryRepository : QueryRepository<SubjectTeacher>, ISubjectTeacherQueryRepository
    {
        public SubjectTeacherQueryRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }
    }
}
