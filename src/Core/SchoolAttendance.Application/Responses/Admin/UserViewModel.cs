using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
	public class UserViewModel
	{
    public UserViewModel()
    {
      AssignedSubjects = new List<int>();
      AssignedRoles = new List<int>();
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public List<int> AssignedRoles { get; set; }
    public string Roles { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string TimeZoneId { get; set; }
    public string AssignedSubjectsInText { get; set; }
    public List<int> AssignedSubjects { get; set; }
  }

  public class StudentExcelContainer
  {

    public StudentExcelContainer()
    {
      Students = new List<UserViewModel>();
    }
    public int Year { get; set; }
    public int GradeId { get; set; }
    public int ClassId { get; set; }
    public int ClassTeacherId { get; set; }

    public List<UserViewModel> Students { get; set; }
  }
}
