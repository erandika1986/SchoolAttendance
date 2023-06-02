using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Enums
{
    public enum AssessmentLevel
    {
        [Description("Grade Level")]
        GradeLevel = 1,
        [Description("Class Level")]
        ClassLevel = 2
    }
}
