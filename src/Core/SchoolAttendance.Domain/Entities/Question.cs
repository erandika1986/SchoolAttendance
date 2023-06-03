using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class Question : BaseEntity
    {
        public Question()
        {
            AssessmentSectionQuestions = new HashSet<AssessmentSectionQuestion>();
            LessonUnitTestTopicQuestions = new HashSet<LessonUnitTestTopicQuestion>();
            QuestionMcqteacherAnswers = new HashSet<QuestionMcqteacherAnswer>();
            QuestionOpenEndedTeacherAnswers = new HashSet<QuestionOpenEndedTeacherAnswer>();
            QuestionStructureds = new HashSet<QuestionStructured>();
        }

        public string Question1 { get; set; }
        public string QuestionRt { get; set; }
        public int QuestionTypeId { get; set; }
        public int OwnerId { get; set; }
        public int AcademicYearId { get; set; }
        public int GradeId { get; set; }
        public int SubjectId { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual User Owner { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<AssessmentSectionQuestion> AssessmentSectionQuestions { get; set; }
        public virtual ICollection<LessonUnitTestTopicQuestion> LessonUnitTestTopicQuestions { get; set; }
        public virtual ICollection<QuestionMcqteacherAnswer> QuestionMcqteacherAnswers { get; set; }
        public virtual ICollection<QuestionOpenEndedTeacherAnswer> QuestionOpenEndedTeacherAnswers { get; set; }
        public virtual ICollection<QuestionStructured> QuestionStructureds { get; set; }
    }
}
