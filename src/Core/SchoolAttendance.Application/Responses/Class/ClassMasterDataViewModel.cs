using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class ClassMasterDataViewModel
  {
    public ClassMasterDataViewModel()
    {
      AcademicYears = new List<DropDownViewModel>();
      Grades = new List<DropDownViewModel>();
      AllTeachers = new List<DropDownViewModel>();
    }

    public List<DropDownViewModel>  AcademicYears { get; set; }
    public List<DropDownViewModel> Grades { get; set; }
    public List<DropDownViewModel> AllTeachers { get; set; }
    public int CurrentAcademicYearId { get; set; }
  }
}
