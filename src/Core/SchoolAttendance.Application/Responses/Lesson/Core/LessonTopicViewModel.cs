using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class LessonTopicViewModel
  {
    public LessonTopicViewModel()
    {
      LessonLectures = new List<LessonLectureViewModel>();
    }

    public int Id { get; set; }
    public int LessonId { get; set; }
    public string Name { get; set; }
    public int SequenceNo { get; set; }
    public bool Editable { get; set; }
    public List<LessonLectureViewModel> LessonLectures { get; set; }
  }
}
