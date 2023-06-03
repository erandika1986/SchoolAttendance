using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class QuestionStructuredTeacherAnswer : BaseEntity
    {
        public int QuestionStructuredId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerTextRt { get; set; }

        public virtual QuestionStructured QuestionStructured { get; set; }
    }
}
