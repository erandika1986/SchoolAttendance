using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class AttendanceReportFilter
  {
    public int FromYear { get; set; }
    public int FromMonth { get; set; }
    public int FromDay { get; set; }

    public int ToYear { get; set; }
    public int ToMonth { get; set; }
    public int ToDay { get; set; }

    public int SelectedYearId { get; set; }
    public int SelectedGradeId { get; set; }
    public int SelectedClassId { get; set; }
    public int SelectedSubjectId { get; set; }
  }
}
