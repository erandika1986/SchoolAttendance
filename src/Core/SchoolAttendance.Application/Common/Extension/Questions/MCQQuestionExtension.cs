
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class MCQQuestionExtension
  {
    public static MCQQuestionViewModel ToMCQVM(this Question model, MCQQuestionViewModel vm=null)
    {
      if(vm==null)
      {
        vm = new MCQQuestionViewModel();
      }

      vm.Id = model.Id;
      vm.Question = model.Question1;
      vm.QuestionRT = model.QuestionRt;
      vm.QuestionType = model.QuestionTypeId;
      vm.AcdemicYearId = model.AcademicYearId;
      vm.GradeId = model.GradeId;
      vm.SubjectId = model.SubjectId;

      foreach (var item in model.QuestionMcqteacherAnswers)
      {
        vm.TeacherAnswers.Add(new QuestionMCQTeacherAnswerViewModel()
        {
          AnswerText = item.AnswerText,
          AnswerTextRT =item.AnswerTextRt,
          Id=item.Id,
          IsCorrectAnswer=item.IsCorrectAnswer,
          QuestionId=item.QuestionId,
          SequenceNo = item.SequenceNo
        });
      }

      return vm;
    }
  }
}
