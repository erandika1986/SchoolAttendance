using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonUnitTestTopicStudentQuestion
    {
        public LessonUnitTestTopicStudentQuestion()
        {
            LessonUnitTestTopicStudentMcqquestionAnswers = new HashSet<LessonUnitTestTopicStudentMcqquestionAnswer>();
            LessonUnitTestTopicStudentOpenEndedQuestionAnswers = new HashSet<LessonUnitTestTopicStudentOpenEndedQuestionAnswer>();
        }

        public int Id { get; set; }
        public int LessonUnitTestTopicQuestionId { get; set; }
        public int StudentId { get; set; }
        public bool? IsCorrect { get; set; }
        public decimal? Score { get; set; }

        public virtual LessonUnitTestTopicQuestion LessonUnitTestTopicQuestion { get; set; }
        public virtual User Student { get; set; }
        public virtual ICollection<LessonUnitTestTopicStudentMcqquestionAnswer> LessonUnitTestTopicStudentMcqquestionAnswers { get; set; }
        public virtual ICollection<LessonUnitTestTopicStudentOpenEndedQuestionAnswer> LessonUnitTestTopicStudentOpenEndedQuestionAnswers { get; set; }
    }
}
