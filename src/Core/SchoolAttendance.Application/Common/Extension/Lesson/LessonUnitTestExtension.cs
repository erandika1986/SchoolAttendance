
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class LessonUnitTestExtension
  {
    public static LessonUnitTestViewModel ToVM(this LessonUnitTest model, LessonUnitTestViewModel vm=null)
    {
      if(vm==null)
      {
        vm = new LessonUnitTestViewModel();
      }

      vm.Id = model.Id;
      vm.LessonId = model.LessonId;
      vm.Name = model.Name;
      vm.StudentGuide = model.StudentGuide;

      foreach (var item in model.LessonUnitTestTopics)
      {
        vm.Topics.Add(item.ToVM());
      }

      return vm;
    }
  }
}
