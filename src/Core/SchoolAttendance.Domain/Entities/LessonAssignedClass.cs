using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonAssignedClass
    {
        public int ClassId { get; set; }
        public int LessonId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public virtual Class Class { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
