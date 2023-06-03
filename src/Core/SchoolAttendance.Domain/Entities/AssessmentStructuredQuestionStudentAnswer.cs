using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class AssessmentStructuredQuestionStudentAnswer : BaseEntity
    {
        public int AssessmentSectionStudentQuestionId { get; set; }
        public int StructuredQuestionId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerTextRt { get; set; }
        public DateTime SubmittedOn { get; set; }

        public virtual AssessmentSectionStudentQuestion AssessmentSectionStudentQuestion { get; set; }
        public virtual QuestionStructured StructuredQuestion { get; set; }
    }
}
