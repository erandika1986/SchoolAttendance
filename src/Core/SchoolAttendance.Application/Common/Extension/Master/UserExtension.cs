
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class UserExtension
  {
    public static UserViewModel ToVm(this User user,UserViewModel vm=null)
    {
      if(vm==null)
      {
        vm = new UserViewModel();
      }

      vm.Id = user.Id;
      vm.FullName = user.FullName;
      vm.Username = user.Username;
      vm.TimeZoneId = user.TimeZoneId;
      vm.Gender = user.Gender;
      vm.AssignedSubjectsInText = string.Join(",", user.SubjectTeachers.Where(s=>!s.DeAllocatedDate.HasValue).Select(ts => ts.Subject.Name).ToList());
      vm.AssignedSubjects = user.SubjectTeachers.Where(x => !x.DeAllocatedDate.HasValue).Select(s => s.SubjectId).ToList();

      var roles = user.UserRoles.ToList();
      vm.Roles = string.Join(",", roles.Select(x => x.Role.Name).ToList());

      foreach (var role in roles)
      {
        vm.AssignedRoles.Add(role.RoleId);
      }
      
      return vm;
    }

    public static User ToModel(this UserViewModel vm, User user=null)
    {
      if(user==null)
      {
        user = new User();
      }

      return user;
    }
  }
}
