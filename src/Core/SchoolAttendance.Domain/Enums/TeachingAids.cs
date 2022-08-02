using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Enums
{
  public enum TeachingAids
  {
    [Description("Video")]
    Video=1,
    [Description("Audio")]
    Audio =2,
    [Description("PDF")]
    PDF =3,
    [Description("Power Point")]
    PowerPoint =4,
    [Description("Word")]
    Word =5
  }
}
