
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class LessonUnitTestTopicExtension
  {
    public static LessonUnitTestTopicViewModel ToVM(this LessonUnitTestTopic model, LessonUnitTestTopicViewModel vm=null)
    {
      if(vm==null)
      {
        vm = new LessonUnitTestTopicViewModel();
      }

      vm.Id = model.Id;
      if(model.LessonUnitTestId.HasValue)
      {
        vm.LessonUnitTestId = model.LessonUnitTestId.Value;
      }

      vm.Name = model.Name;
      vm.Instruction = model.Instruction;
      vm.QuestionTypeId = model.QuestionTypeId;

      foreach (var item in model.LessonUnitTestTopicQuestions)
      {
        vm.Questions.Add(item.ToVM());
      }

      return vm;
    }
  }
}
