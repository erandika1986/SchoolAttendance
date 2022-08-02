using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class ClassSubjectViewModel
  {
    public ClassSubjectViewModel()
    {
      SubjectTeachers = new List<DropDownViewModel>();
    }


    public int ClassId { get; set; }

    public int SubjectId { get; set; }
    public string SubjectName { get; set; }

    public int SubjectTeacherId { get; set; }
    public List<DropDownViewModel> SubjectTeachers { get; set; }
  }
}
