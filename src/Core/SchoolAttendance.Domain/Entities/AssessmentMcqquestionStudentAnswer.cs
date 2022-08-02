using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class AssessmentMcqquestionStudentAnswer
    {
        public int Id { get; set; }
        public int AssessmentSectionStudentQuestionId { get; set; }
        public int TeacherAnswerId { get; set; }
        public bool IsSelected { get; set; }
        public bool IsSelectionCorrect { get; set; }
        public DateTime SubmittedOn { get; set; }

        public virtual AssessmentSectionStudentQuestion AssessmentSectionStudentQuestion { get; set; }
        public virtual QuestionMcqteacherAnswer TeacherAnswer { get; set; }
    }
}
