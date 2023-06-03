using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class AssessmentSectionStudentQuestion : BaseEntity
    {
        public AssessmentSectionStudentQuestion()
        {
            AssessmentMcqquestionStudentAnswers = new HashSet<AssessmentMcqquestionStudentAnswer>();
            AssessmentOpenEndedQuestionStudentAnswers = new HashSet<AssessmentOpenEndedQuestionStudentAnswer>();
            AssessmentStructuredQuestionStudentAnswers = new HashSet<AssessmentStructuredQuestionStudentAnswer>();
        }

        public int AssessmentSectionQuestionId { get; set; }
        public int StudentId { get; set; }
        public bool? IsCorrect { get; set; }
        public decimal? Score { get; set; }

        public virtual AssessmentSectionQuestion AssessmentSectionQuestion { get; set; }
        public virtual User Student { get; set; }
        public virtual ICollection<AssessmentMcqquestionStudentAnswer> AssessmentMcqquestionStudentAnswers { get; set; }
        public virtual ICollection<AssessmentOpenEndedQuestionStudentAnswer> AssessmentOpenEndedQuestionStudentAnswers { get; set; }
        public virtual ICollection<AssessmentStructuredQuestionStudentAnswer> AssessmentStructuredQuestionStudentAnswers { get; set; }
    }
}
