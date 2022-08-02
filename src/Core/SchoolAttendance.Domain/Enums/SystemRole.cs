using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Enums
{
  public enum SystemRole
  {

    [Description("Admin")]
    Admin = 1,
    [Description("Principle")]
    Principle = 2,
    [Description("Vice Principle")]
    VicePrinciple = 3,
    [Description("Level Head")]
    LevelHead = 4,
    [Description("Department Head")]
    DepartmentHead = 5,
    [Description("Teacher")]
    Teacher = 6,
    [Description("Student")]
    Student = 7,
    [Description("Parent")]
    Parent = 8
  }
}
