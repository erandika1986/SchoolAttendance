using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonLectureContentType
    {
        public LessonLectureContentType()
        {
            LessonLectures = new HashSet<LessonLecture>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<LessonLecture> LessonLectures { get; set; }
    }
}
