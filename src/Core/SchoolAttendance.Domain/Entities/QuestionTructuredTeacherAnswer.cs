using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class QuestionTructuredTeacherAnswer
    {
        public int Id { get; set; }
        public int QuestionStructuredId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerTextRt { get; set; }

        public virtual QuestionStructured QuestionStructured { get; set; }
    }
}
