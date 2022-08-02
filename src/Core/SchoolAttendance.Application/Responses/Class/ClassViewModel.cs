using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class ClassViewModel
  {
    public ClassViewModel()
    {
      ClassSubjects = new List<ClassSubjectViewModel>();
    }

    public int Id { get; set; }
    public int SelectedAcademicYearId { get; set; }
    public int SelectedGradeId { get; set; }
    public int SelectedClassTeacherId { get; set; }
    public string Name { get; set; }
    public List<ClassSubjectViewModel> ClassSubjects { get; set; }
  }


}
