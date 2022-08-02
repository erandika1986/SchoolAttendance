using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class QuestionStructured
    {
        public QuestionStructured()
        {
            AssessmentStructuredQuestionStudentAnswers = new HashSet<AssessmentStructuredQuestionStudentAnswer>();
            QuestionTructuredTeacherAnswers = new HashSet<QuestionTructuredTeacherAnswer>();
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionTextRt { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<AssessmentStructuredQuestionStudentAnswer> AssessmentStructuredQuestionStudentAnswers { get; set; }
        public virtual ICollection<QuestionTructuredTeacherAnswer> QuestionTructuredTeacherAnswers { get; set; }
    }
}
