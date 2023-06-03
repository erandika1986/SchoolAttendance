using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class QuestionType : BaseEntity
    {
        public QuestionType()
        {
            LessonUnitTestTopics = new HashSet<LessonUnitTestTopic>();
        }

        public string Name { get; set; }

        public virtual ICollection<LessonUnitTestTopic> LessonUnitTestTopics { get; set; }
    }
}
