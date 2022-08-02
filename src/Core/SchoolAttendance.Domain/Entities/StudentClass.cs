using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public class StudentClass
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Class Class { get; set; }
        public virtual User Student { get; set; }
    }
}
