using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class BasicAttendanceViewModel
  {
    public int Id { get; set; }
    public string ClassName { get; set; }
    public string SubjectName { get; set; }
    public string Date { get; set; }
    public int TotalAttendedStudents { get; set; }
    public int TotalAbsenceStudents { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string SubjectTeacherName { get; set; }
  }
}
