using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class Audience
  {
    public string Secret { get; set; }
    public string Iss { get; set; }
    public string Aud { get; set; }
  }
}
