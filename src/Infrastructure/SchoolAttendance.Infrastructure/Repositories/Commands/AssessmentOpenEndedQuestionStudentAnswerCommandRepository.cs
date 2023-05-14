using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Infrastructure.Data;
using SchoolAttendance.Infrastructure.Repositories.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Repositories.Commands
{
    public class AssessmentOpenEndedQuestionStudentAnswerCommandRepository  :  
        CommandRepository<AssessmentOpenEndedQuestionStudentAnswer>, IAssessmentOpenEndedQuestionStudentAnswerCommandRepository
    {
        public AssessmentOpenEndedQuestionStudentAnswerCommandRepository(SchoolAttendanceContext context) : base(context)
        {
            
        }
    }
}
