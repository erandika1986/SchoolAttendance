using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public class SubjectAttendance
    {
        public SubjectAttendance()
        {
            StudentSubjectAttendances = new HashSet<StudentSubjectAttendance>();
        }

        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int? TimeSlotId { get; set; }
        public bool IsExtraClass { get; set; }
        public bool IsReScheduleClass { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string LessonDetails { get; set; }
        public string UsedSoftwareName { get; set; }
        public DateTime ActualEnteredDate { get; set; }

        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ClassSubjectTimeTable TimeSlot { get; set; }
        public virtual ICollection<StudentSubjectAttendance> StudentSubjectAttendances { get; set; }
    }
}
