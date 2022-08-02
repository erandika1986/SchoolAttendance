using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonAssignment
    {
        public LessonAssignment()
        {
            LessonAssignmentStudents = new HashSet<LessonAssignmentStudent>();
        }

        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }
        public string ClosingDateTime { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<LessonAssignmentStudent> LessonAssignmentStudents { get; set; }
    }
}
