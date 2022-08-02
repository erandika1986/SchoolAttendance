using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class LessonListFilterMasterData
  {
    public LessonListFilterMasterData()
    {
      AcademicYears = new List<DropDownViewModel>();
      Grades = new List<DropDownViewModel>();
      LessonStatuses = new List<DropDownViewModel>();
      TeacherAids = new List<DropDownViewModel>();
    }
    public int CurrentAcademicYear { get; set; }
    public int SelectedGradeId { get; set; }

    public List<DropDownViewModel> AcademicYears { get; set; }
    public List<DropDownViewModel> Grades { get; set; }
    public List<DropDownViewModel> LessonStatuses { get; set; }
    public List<DropDownViewModel> TeacherAids { get; set; }

  }
}
