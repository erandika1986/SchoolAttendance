using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Constants
{
  public static class AuthorizedRoles
  {

    public const string Admin = "Admin";
    public const string Principle = "Principle";
    public const string VicePrinciple = "VicePrinciple";
    public const string LevelHead = "LevelHead";
    public const string Teacher = "Teacher";
    public const string Student = "Student";
    public const string Parent = "Parent";
    public const string AllAuthorizedUser = "Admin,Principle,VicePrinciple,LevelHead,DepartmentHead,Teacher,Student,Parent";
    public const string AuthorizedForAdminAndTeacher = "Admin,Principle,VicePrinciple,LevelHead,DepartmentHead,Teacher";
  }
}
