using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class PasswordUpdateViewModel
  {
    public int Id { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
  }
}
