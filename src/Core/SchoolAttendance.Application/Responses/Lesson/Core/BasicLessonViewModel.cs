using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class BasicLessonViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Owner { get; set; }
    public int AcademicYear { get; set; }
    public string GradeName { get; set; }
    public string Subject { get; set; }
    public string CreatedOn { get; set; }
    public string Status { get; set; }
  }
}
