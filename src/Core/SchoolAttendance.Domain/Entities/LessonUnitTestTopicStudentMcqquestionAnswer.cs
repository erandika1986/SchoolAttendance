using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class LessonUnitTestTopicStudentMcqquestionAnswer
    {
        public int Id { get; set; }
        public int LessonUnitTestTopicStudentQuestionId { get; set; }
        public int TeacherAnswerId { get; set; }
        public bool IsSelected { get; set; }
        public bool IsSelectionCorrect { get; set; }

        public virtual LessonUnitTestTopicStudentQuestion LessonUnitTestTopicStudentQuestion { get; set; }
        public virtual QuestionMcqteacherAnswer TeacherAnswer { get; set; }
    }
}
