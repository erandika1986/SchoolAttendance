using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonUnitTestTopic
    {
        public LessonUnitTestTopic()
        {
            LessonUnitTestTopicQuestions = new HashSet<LessonUnitTestTopicQuestion>();
        }

        public int Id { get; set; }
        public int? LessonUnitTestId { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public int QuestionTypeId { get; set; }

        public virtual LessonUnitTest LessonUnitTest { get; set; }
        public virtual QuestionType QuestionType { get; set; }
        public virtual ICollection<LessonUnitTestTopicQuestion> LessonUnitTestTopicQuestions { get; set; }
    }
}
