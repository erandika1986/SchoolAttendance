using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class QuestionStructured : BaseEntity
    {
        public QuestionStructured()
        {
            AssessmentStructuredQuestionStudentAnswers = new HashSet<AssessmentStructuredQuestionStudentAnswer>();
            QuestionTructuredTeacherAnswers = new HashSet<QuestionStructuredTeacherAnswer>();
        }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionTextRt { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<AssessmentStructuredQuestionStudentAnswer> AssessmentStructuredQuestionStudentAnswers { get; set; }
        public virtual ICollection<QuestionStructuredTeacherAnswer> QuestionTructuredTeacherAnswers { get; set; }
    }
}
