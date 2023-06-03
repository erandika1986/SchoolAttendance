using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class QuestionMcqteacherAnswer : BaseEntity
    {
        public QuestionMcqteacherAnswer()
        {
            AssessmentMcqquestionStudentAnswers = new HashSet<AssessmentMcqquestionStudentAnswer>();
            LessonUnitTestTopicStudentMcqquestionAnswers = new HashSet<LessonUnitTestTopicStudentMcqquestionAnswer>();
        }

        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerTextRt { get; set; }
        public int SequenceNo { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<AssessmentMcqquestionStudentAnswer> AssessmentMcqquestionStudentAnswers { get; set; }
        public virtual ICollection<LessonUnitTestTopicStudentMcqquestionAnswer> LessonUnitTestTopicStudentMcqquestionAnswers { get; set; }
    }
}
