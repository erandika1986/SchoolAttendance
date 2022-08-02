using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class AttendanceListFilterMasterData
  {
    public AttendanceListFilterMasterData()
    {
      AcademicYears = new List<DropDownViewModel>();
      Grades = new List<DropDownViewModel>() ;
      Classes = new List<DropDownViewModel>();
      Subjects = new List<DropDownViewModel>();
    }
    public int CurrentAcademicYear { get; set; }
    public int SelectedGradeId { get; set; }
    public int SelectedClassId { get; set; }
    public int SelectedSubjectId { get; set; }

    public List<DropDownViewModel> AcademicYears { get; set; }
    public List<DropDownViewModel> Grades { get; set; }
    public List<DropDownViewModel> Classes { get; set; }
    public List<DropDownViewModel> Subjects { get; set; }
  }
}
