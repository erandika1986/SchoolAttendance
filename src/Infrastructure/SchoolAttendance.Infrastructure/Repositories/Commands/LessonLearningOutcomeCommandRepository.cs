using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Infrastructure.Data;
using SchoolAttendance.Infrastructure.Repositories.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Repositories.Command
{
    public class LessonLearningOutcomeCommandRepository :
        CommandRepository<LessonLearningOutcome>, ILessonLearningOutcomeCommandRepository
    {
        public LessonLearningOutcomeCommandRepository(SchoolAttendanceContext context) : base(context)
        {
            
        }
    }
}
