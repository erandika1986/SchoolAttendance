
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class LessonUnitTestTopicQuestionExtension
  {
    public static LessonUnitTestTopicQuestionViewModel ToVM(this LessonUnitTestTopicQuestion model, LessonUnitTestTopicQuestionViewModel vm=null)
    {
      if(vm==null)
      {
        vm = new LessonUnitTestTopicQuestionViewModel();
      }

      vm.Id = model.Id;
      vm.SequenceNo = model.SequenceNo;

      if(model.Score.HasValue)
      {
        vm.Score = model.Score.Value;
      }


      if (model.LessonUnitTestTopic.LessonUnitTest.Lesson.AcademicYearId.HasValue)
      {
        vm.AcademicYearId = model.LessonUnitTestTopic.LessonUnitTest.Lesson.AcademicYearId.Value;
      }

      if(model.LessonUnitTestTopic.LessonUnitTest.Lesson.GradeId.HasValue)
      {
        vm.GradeId = model.LessonUnitTestTopic.LessonUnitTest.Lesson.GradeId.Value;
      }

      if(model.LessonUnitTestTopic.LessonUnitTest.Lesson.SubjectId.HasValue)
      {
        vm.SubjectId = model.LessonUnitTestTopic.LessonUnitTest.Lesson.SubjectId.Value;
      }

      if(model.Question.QuestionTypeId==(int)SchoolAttendance.Domain.Enums.QuestionType.MCQ)
      {
        vm.MCQQuestion = model.Question.ToMCQVM();
      }
      else if(model.Question.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.OpenEnded)
      {
        vm.OpenEndedQuestion = model.Question.ToOpenEndedVM();
      }
     
      return vm;
    }
  }
}
