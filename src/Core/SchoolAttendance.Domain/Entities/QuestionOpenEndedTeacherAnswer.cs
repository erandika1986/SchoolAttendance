using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class QuestionOpenEndedTeacherAnswer : BaseEntity
    {
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerTextRt { get; set; }

        public virtual Question Question { get; set; }
    }
}
