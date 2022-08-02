using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class MCQQuestionViewModel: BaseQuestionViewModel
  {
    public MCQQuestionViewModel()
    {
      TeacherAnswers = new List<QuestionMCQTeacherAnswerViewModel>();
    }

    public List<QuestionMCQTeacherAnswerViewModel> TeacherAnswers { get; set; }
  }
}
