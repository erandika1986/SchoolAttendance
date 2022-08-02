using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class StudentViewModel
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string Role { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string TimeZoneId { get; set; }

    public int AcademicYearId { get; set; }
    public int GradeId { get; set; }
    public int ClassId { get; set; }
  }
}
