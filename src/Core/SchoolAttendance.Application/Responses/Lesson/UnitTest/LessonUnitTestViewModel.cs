using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class LessonUnitTestViewModel
  {
    public LessonUnitTestViewModel()
    {
      Topics = new List<LessonUnitTestTopicViewModel>();
    }
    public int Id { get; set; }
    public int LessonId { get; set; }
    public string Name { get; set; }
    public string StudentGuide { get; set; }

    public List<LessonUnitTestTopicViewModel> Topics { get; set; }
  }
}
