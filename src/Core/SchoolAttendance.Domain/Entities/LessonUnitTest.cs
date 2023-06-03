using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonUnitTest : BaseEntity
    {
        public LessonUnitTest()
        {
            LessonUnitTestTopics = new HashSet<LessonUnitTestTopic>();
        }

        public string Name { get; set; }
        public string StudentGuide { get; set; }
        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<LessonUnitTestTopic> LessonUnitTestTopics { get; set; }
    }
}
