using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class AttendanceFilterViewModel
  {
    public int Id { get; set; }
    public int GradeId { get; set; }
    public int ClassId { get; set; }
    public int SubjectId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public int StartHour { get; set; }
    public int StartMin { get; set; }
    public int EndHour { get; set; }
    public int EndMin { get; set; }
  }
}
