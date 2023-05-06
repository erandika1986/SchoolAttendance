using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionType = SchoolAttendance.Domain.Entities.QuestionType;

namespace SchoolAttendance.Domain.Repositories.Query
{
    public interface IQuestionTypeQueryRepository : IQueryRepository<QuestionType>
    {
    }
}
