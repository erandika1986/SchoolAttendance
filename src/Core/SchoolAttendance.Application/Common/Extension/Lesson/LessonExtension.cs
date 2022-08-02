
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class LessonExtension
  {
    public static LessonViewModel ToVM(this Lesson model, LessonViewModel vm=null)
    {
      if(vm==null)
      {
        vm = new LessonViewModel();
      }

      vm.Id = model.Id;
      vm.LessonDetail.LessonId = model.Id;
      vm.LessonDetail.Name = model.Name;
      vm.LessonDetail.LessonIntroduction = model.LessonIntroduction;
      vm.LessonDetail.CompetencyLevel = model.CompetencyLevel;
      vm.LessonDetail.HasLessonTest = model.HasLessonTest;

      if(!string.IsNullOrEmpty(model.TeachingAids))
      {
        vm.LessonDetail.TeacherAids = model.TeachingAids.Split(",").Select(x=>int.Parse(x)).ToList();
      }

      vm.LessonDetail.OwnerId = model.LessonOwnerId;
      vm.LessonDetail.LessonStatus = model.Status;
      vm.LessonDetail.TeachingProcess = model.TeachingProcess;
      vm.LessonDetail.AssignedClasses = model.LessonAssignedClasses.Select(x => x.ClassId).ToList();

      if (model.AcademicYearId.HasValue)
      {
        vm.LessonDetail.AcademicYearId = model.AcademicYearId.Value;
      }

      if(model.GradeId.HasValue)
      {
        vm.LessonDetail.GradeId = model.GradeId.Value;
      }

      if(model.SubjectId.HasValue)
      {
        vm.LessonDetail.SubjectId = model.SubjectId.Value;
      }

      if(model.Duration.HasValue)
      {
        vm.LessonDetail.Duration = model.Duration.Value;
      }

      foreach (var preRequisites in model.LessonPrerequisites)
      {
        vm.LessonPrerequisiteForm.LessonPrerequisites.Add(new LessonPrerequisiteViewModel()
        {
          Id =preRequisites.Id,
          Prerequisite = preRequisites.Prerequisite
        });
      }

      foreach (var outCome in model.LessonLearningOutcomes)
      {
        vm.LessonOutcomeForm.LessonOutcomes.Add(new LessonLearningOutcomeViewModel()
        {
          Id = outCome.Id,
          LessonOutcome =outCome.LearningOutcome
        });
      }

      foreach (var item in model.LessonTopics)
      {
        vm.LessonTopicForm.LessonTopics.Add(item.ToVM());
      }

      var lessonUnitTest = model.LessonUnitTests.FirstOrDefault();
      if(lessonUnitTest!=null)
      {
        vm.LessonUnitTest = lessonUnitTest.ToVM();
      }


      return vm;
    }
  }
}
