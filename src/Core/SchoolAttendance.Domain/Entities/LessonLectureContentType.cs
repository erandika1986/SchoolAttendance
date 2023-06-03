using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonLectureContentType : BaseEntity
    {
        public LessonLectureContentType()
        {
            LessonLectures = new HashSet<LessonLecture>();
        }

        public string Name { get; set; }
        public string IconPath { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<LessonLecture> LessonLectures { get; set; }
    }
}
