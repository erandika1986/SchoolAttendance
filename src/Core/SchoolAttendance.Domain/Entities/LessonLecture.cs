using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonLecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TopicId { get; set; }
        public int? LectureContentTypeId { get; set; }
        public string LectureContent { get; set; }
        public string Mimetype { get; set; }

        public virtual LessonLectureContentType LectureContentType { get; set; }
        public virtual LessonTopic Topic { get; set; }
    }
}
