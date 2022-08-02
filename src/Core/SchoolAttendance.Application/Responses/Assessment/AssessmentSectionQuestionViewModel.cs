using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class AssessmentSectionQuestionViewModel
  {
    public int Id { get; set; }
    public int SequenceId { get; set; }
    public decimal Score { get; set; }

    public MCQQuestionViewModel MCQQuestion { get; set; }
    public OpenEndedQuestionViewModel OpenEndedQuestion { get; set; }
    public QuestionStructuredViewModel StructuredQuestion { get; set; }
  }
}
