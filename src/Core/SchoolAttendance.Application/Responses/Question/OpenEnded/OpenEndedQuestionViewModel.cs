using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class OpenEndedQuestionViewModel:BaseQuestionViewModel
  {
    public OpenEndedQuestionViewModel()
    {
      TeacherAnswers = new List<QuestionOpneEndedTeacherAnswerViewModel>();
    }
    public List<QuestionOpneEndedTeacherAnswerViewModel> TeacherAnswers { get; set; }
  }
}
