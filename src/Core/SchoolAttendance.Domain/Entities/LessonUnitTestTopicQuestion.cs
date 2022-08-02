using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonUnitTestTopicQuestion
    {
        public LessonUnitTestTopicQuestion()
        {
            LessonUnitTestTopicStudentQuestions = new HashSet<LessonUnitTestTopicStudentQuestion>();
        }

        public int Id { get; set; }
        public int LessonUnitTestTopicId { get; set; }
        public int QuestionId { get; set; }
        public int SequenceNo { get; set; }
        public decimal? Score { get; set; }

        public virtual LessonUnitTestTopic LessonUnitTestTopic { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<LessonUnitTestTopicStudentQuestion> LessonUnitTestTopicStudentQuestions { get; set; }
    }
}
