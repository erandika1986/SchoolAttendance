using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class OpenEndedQuestionExtension
  {
    public static OpenEndedQuestionViewModel ToOpenEndedVM(this Question model, OpenEndedQuestionViewModel vm = null)
    {
      if (vm == null)
      {
        vm = new OpenEndedQuestionViewModel();
      }

      vm.Id = model.Id;
      vm.Question = model.Question1;
      vm.QuestionRT = model.QuestionRt;
      vm.QuestionType = model.QuestionTypeId;
      vm.AcdemicYearId = model.AcademicYearId;
      vm.GradeId = model.GradeId;
      vm.SubjectId = model.SubjectId;

      foreach (var item in model.QuestionOpenEndedTeacherAnswers)
      {
        vm.TeacherAnswers.Add(new QuestionOpneEndedTeacherAnswerViewModel()
        {
          AnswerText = item.AnswerText,
          AnswerTextRT = item.AnswerTextRt,
          Id = item.Id
        });
      }

      return vm;
    }
  }
}
