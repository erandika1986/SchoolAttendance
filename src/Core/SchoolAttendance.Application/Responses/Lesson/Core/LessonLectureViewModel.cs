using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class LessonLectureViewModel
  {
    public int Id { get; set; }
    public int TopicId { get; set; }
    public string Name { get; set; }
    public int ContentType { get; set; }
    public string MimeType { get; set; }
    public string Content { get; set; }
    public bool Editable { get; set; }
  }
}
