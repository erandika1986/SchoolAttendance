using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class LessonFilter
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string SearchText { get; set; }
    public int AcademicYear { get; set; }
    public int GradeId { get; set; }
    public int SubjectId { get; set; }
  }
}
