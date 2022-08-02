using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class LessonUnitTestTopicViewModel
  {
    public LessonUnitTestTopicViewModel()
    {
      Questions = new List<LessonUnitTestTopicQuestionViewModel>();
    }

    public int Id { get; set; }
    public int LessonUnitTestId { get; set; }
    public string Name { get; set; }
    public string Instruction { get; set; }
    public int QuestionTypeId { get; set; }

    public bool Editable { get; set; }
    public List<LessonUnitTestTopicQuestionViewModel> Questions { get; set; }
  }
}
