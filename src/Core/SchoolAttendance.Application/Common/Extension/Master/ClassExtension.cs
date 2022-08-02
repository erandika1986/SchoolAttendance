
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class ClassExtension
  {
    public static BasicClassDetailViewModel ToVm(this Class model,BasicClassDetailViewModel vm=null)
    {
      if(vm==null)
      {
        vm = new BasicClassDetailViewModel();
      }

      vm.ClassTeacherName = model.ClassTeacher.FullName;
      vm.Id = model.Id;
      vm.Name = model.Name;
      vm.TotalStudentCount = model.StudentClasses.Where(x => x.IsActive == true).Count();
      vm.AcademicYearId = model.AcademicYear;
      vm.GradeId = model.GradeId;

      return vm;
    }
  }
}
