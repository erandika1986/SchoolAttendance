using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonAssignmentStudent
    {
        public LessonAssignmentStudent()
        {
            LessonAssignmentStudentUploads = new HashSet<LessonAssignmentStudentUpload>();
        }

        public int Id { get; set; }
        public int LessonAssignmentId { get; set; }
        public int StudentId { get; set; }
        public string StudentRemarks { get; set; }
        public string TeacherComment { get; set; }
        public decimal? Score { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string StudentIp { get; set; }
        public string StudentBrowser { get; set; }

        public virtual LessonAssignment LessonAssignment { get; set; }
        public virtual User Student { get; set; }
        public virtual ICollection<LessonAssignmentStudentUpload> LessonAssignmentStudentUploads { get; set; }
    }
}
