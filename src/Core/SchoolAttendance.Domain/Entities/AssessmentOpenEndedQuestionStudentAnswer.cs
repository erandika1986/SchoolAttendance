using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class AssessmentOpenEndedQuestionStudentAnswer
    {
        public int Id { get; set; }
        public int AssessmentSectionStudentQuestionId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerTextRt { get; set; }
        public string TeacherComment { get; set; }
        public DateTime? SubmittedOn { get; set; }

        public virtual AssessmentSectionStudentQuestion AssessmentSectionStudentQuestion { get; set; }
    }
}
