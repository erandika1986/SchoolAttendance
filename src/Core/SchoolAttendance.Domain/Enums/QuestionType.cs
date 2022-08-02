using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Enums
{
  public enum QuestionType
  {
    [Description("Multiple Choice Question")]
    MCQ=1,
    [Description("Open Ended")]
    OpenEnded =2,
    [Description("Sturctured")]
    Sturctured =3
  }
}
