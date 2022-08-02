using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class AssessmentSectionQuestion
    {
        public AssessmentSectionQuestion()
        {
            AssessmentSectionStudentQuestions = new HashSet<AssessmentSectionStudentQuestion>();
        }

        public int Id { get; set; }
        public int AssessementSectionId { get; set; }
        public int QuestionId { get; set; }
        public int SequenceNo { get; set; }
        public decimal Score { get; set; }

        public virtual AssessmentSection AssessementSection { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<AssessmentSectionStudentQuestion> AssessmentSectionStudentQuestions { get; set; }
    }
}
