using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class QuestionOpneEndedTeacherAnswerViewModel
  {
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string AnswerText { get; set; }
    public string AnswerTextRT { get; set; }
  }
}
