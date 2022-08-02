using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class BasicStudentViewModel
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string IndexNo { get; set; }
    public string Password { get; set; }
    public string Year { get; set; }
    public string Grade { get; set; }
    public string ClassName { get; set; }
    public string TimeZoneId { get; set; }
  }
}
