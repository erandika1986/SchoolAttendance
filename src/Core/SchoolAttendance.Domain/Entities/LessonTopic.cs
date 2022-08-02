using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonTopic
    {
        public LessonTopic()
        {
            LessonLectures = new HashSet<LessonLecture>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? LessonId { get; set; }
        public int SequenceNo { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<LessonLecture> LessonLectures { get; set; }
    }
}
