using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class QuestionStructuredViewModel:BaseQuestionViewModel
  {
    public QuestionStructuredViewModel()
    {
      SubQuestions = new List<SubQuestionViewModel>();
    }
    public List<SubQuestionViewModel> SubQuestions { get; set; }
  }


}
