using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Enums
{
    public enum AssessmentConductBy
    {
        [Description("By Government")]
        ByGovernment = 1,

        [Description("By School")]
        BySchool = 2
    }
}
