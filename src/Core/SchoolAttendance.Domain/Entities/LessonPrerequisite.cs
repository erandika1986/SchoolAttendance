using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonPrerequisite
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Prerequisite { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
