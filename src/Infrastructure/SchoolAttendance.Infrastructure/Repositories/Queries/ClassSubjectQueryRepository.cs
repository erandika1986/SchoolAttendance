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
    public class ClassSubjectQueryRepository 
        : QueryRepository<ClassSubject>, IClassSubjectQueryRepository
    {
        public ClassSubjectQueryRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }

        public async Task<List<ClassSubject>> GetClassForSelectedSubjectTeacher(int academicYear, int subjectTeacherId, CancellationToken cancellationToken)
        {
           var classSubjects = await _context
                .ClassSubjects
                .Where(x => x.Class.AcademicYear == academicYear && x.SubjectTeacherId == subjectTeacherId)
                .ToListAsync(cancellationToken); 
            
            return classSubjects;
        }
    }
}
