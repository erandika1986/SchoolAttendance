using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonAssignmentStudentUpload : BaseEntity
    {
        public int LessonAssignmentStudentId { get; set; }
        public string UploadFilePath { get; set; }
        public DateTime UploadedOn { get; set; }
        public bool IsActive { get; set; }

        public virtual LessonAssignmentStudent LessonAssignmentStudent { get; set; }
    }
}
