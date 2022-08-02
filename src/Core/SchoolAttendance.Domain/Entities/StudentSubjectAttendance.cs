using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class StudentSubjectAttendance
    {
        public int SubjectAttendanceId { get; set; }
        public int StudentId { get; set; }
        public bool IsAttended { get; set; }

        public virtual User Student { get; set; }
        public virtual SubjectAttendance SubjectAttendance { get; set; }
    }
}
