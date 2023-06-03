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
    public class LessonQueryRepository  : QueryRepository<Lesson>, ILessonQueryRepository
    {
        public LessonQueryRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }

        public  IOrderedQueryable<Lesson> GetLessonsByOwnerId(int ownerId)
        {
            var query = _context
                .Lessons.Where(x => x.LessonOwnerId == ownerId)
                .OrderByDescending(x => x.CreatedOn);

            return query;

        }
    }
}
