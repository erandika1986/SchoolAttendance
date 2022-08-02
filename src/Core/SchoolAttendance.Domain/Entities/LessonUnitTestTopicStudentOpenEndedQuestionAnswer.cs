using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonUnitTestTopicStudentOpenEndedQuestionAnswer
    {
        public int Id { get; set; }
        public int LessonUnitTestTopicStudentQuestionId { get; set; }
        public string Answer { get; set; }
        public string AnswerRt { get; set; }
        public string TeacherComment { get; set; }

        public virtual LessonUnitTestTopicStudentQuestion LessonUnitTestTopicStudentQuestion { get; set; }
    }
}
