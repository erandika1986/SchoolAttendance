
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class LessonTopicExtension
  {
    public static LessonTopicViewModel ToVM(this LessonTopic model,LessonTopicViewModel vm=null)
    {
      if(vm==null)
      {
        vm = new LessonTopicViewModel();
      }

      vm.Id = model.Id;
      vm.LessonId = model.LessonId.Value;
      vm.Name = model.Name;
      vm.SequenceNo = model.SequenceNo;

      foreach (var item in model.LessonLectures)
      {
        vm.LessonLectures.Add(item.ToVM());
      }

      return vm;
    }
  }
}
