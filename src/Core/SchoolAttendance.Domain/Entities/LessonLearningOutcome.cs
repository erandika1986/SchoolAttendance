using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonLearningOutcome
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string LearningOutcome { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
