using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class GradeViewModel
  {
    public GradeViewModel()
    {
      GradeSubjects = new List<int>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public string LevelHeadName { get; set; }
    public int LevelHeadId { get; set; }

    public string GradeSubjectsText { get; set; }
    public List<int> GradeSubjects { get; set; }
  }
}
