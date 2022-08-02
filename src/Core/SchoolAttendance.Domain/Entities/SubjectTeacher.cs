using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public class SubjectTeacher
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? DeAllocatedDate { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual User Teacher { get; set; }
    }
}
