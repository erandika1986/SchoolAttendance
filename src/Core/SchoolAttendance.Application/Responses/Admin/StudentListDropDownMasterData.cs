using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class StudentListDropDownMasterData
  {
    public StudentListDropDownMasterData()
    {
      AcademicYears = new List<DropDownViewModel>();
      Grades = new List<DropDownViewModel>() { new DropDownViewModel() { Id = 0, Name = "---All---" } };
      Classes = new List<DropDownViewModel>() { new DropDownViewModel() { Id = 0, Name = "---All---" } };
    }

    public int CurrentAcademicYear { get; set; }
    public int SelectedGradeId { get; set; }
    public int SelectedClassId { get; set; }

    public List<DropDownViewModel> AcademicYears { get; set; }
    public List<DropDownViewModel> Grades { get; set; }
    public List<DropDownViewModel> Classes { get; set; }
  }
}
