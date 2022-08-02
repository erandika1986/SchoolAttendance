using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonUnitTest
    {
        public LessonUnitTest()
        {
            LessonUnitTestTopics = new HashSet<LessonUnitTestTopic>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string StudentGuide { get; set; }
        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<LessonUnitTestTopic> LessonUnitTestTopics { get; set; }
    }
}
