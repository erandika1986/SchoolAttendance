using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class ClassTeacheeDropDownMasterData
  {
    public ClassTeacheeDropDownMasterData()
    {
      AcademicYears = new List<DropDownViewModel>();
      Grades = new List<DropDownViewModel>();
      Classes = new List<DropDownViewModel>();
    }


    public List<DropDownViewModel> AcademicYears { get; set; }
    public List<DropDownViewModel> Grades { get; set; }
    public List<DropDownViewModel> Classes { get; set; }

    public int SelectedYearId { get; set; }
    public int SelectedGradeId { get; set; }
    public int SelectedClassId { get; set; }
    public int SelectedSubjectId { get; set; }
    public string Role { get; set; }

    public int FromYear { get; set; }
    public int FromMonth { get; set; }
    public int FromDay { get; set; }

    public int ToYear { get; set; }
    public int ToMonth { get; set; }
    public int ToDay { get; set; }
  }
}
