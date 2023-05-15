using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Query;
using SchoolAttendance.Infrastructure.Data;
using SchoolAttendance.Infrastructure.Repositories.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionType = SchoolAttendance.Domain.Entities.QuestionType;

namespace SchoolAttendance.Infrastructure.Repositories.Queries
{
    public class QuestionTypeQueryRepository 
        : QueryRepository<QuestionType>, IQuestionTypeQueryRepository
    {
        public QuestionTypeQueryRepository(SchoolAttendanceContext context)
            : base(context)
        {
            
        }
    }
}
