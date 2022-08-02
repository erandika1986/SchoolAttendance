using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class SubQuestionViewModel
  {
    public SubQuestionViewModel()
    {
      TeacherAnswers = new List<SubQuestionTeacherAnswerViewModel>();
    }

    public int StructuredQuestionId { get; set; }
    public string QuestionText { get; set; }
    public string QuestionTextRT { get; set; }

    public List<SubQuestionTeacherAnswerViewModel> TeacherAnswers { get; set; }
  }
}
