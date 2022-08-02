using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class LessonViewModel
  {
    public LessonViewModel()
    {
      LessonDetail = new LessonDetailViewModel();
      LessonPrerequisiteForm = new LessonPrerequisiteForm();
      LessonOutcomeForm = new LessonOutcomeForm();
      LessonTopicForm = new LessonTopicForm();
      LessonUnitTest = new LessonUnitTestViewModel();


    }
    public int Id { get; set; }
    public LessonDetailViewModel LessonDetail { get; set; }
    public LessonPrerequisiteForm LessonPrerequisiteForm { get; set; }
    public LessonOutcomeForm LessonOutcomeForm { get; set; }
    public LessonTopicForm LessonTopicForm { get; set; }
    public LessonUnitTestViewModel LessonUnitTest { get; set; }

  }

  public class LessonDetailViewModel
  {
    public LessonDetailViewModel()
    {
      AssignedClasses = new List<int>();
      TeacherAids = new List<int>();
    }
    public int LessonId { get; set; }
    public string Name { get; set; }
    public string LessonIntroduction { get; set; }
    public decimal Duration { get; set; }
    public string CompetencyLevel { get; set; }
    public List<int> TeacherAids { get; set; }
    public string TeachingProcess { get; set; }
    public int OwnerId { get; set; }
    public int AcademicYearId { get; set; }
    public int GradeId { get; set; }
    public int SubjectId { get; set; }
    public int LessonStatus { get; set; }
    public List<int> AssignedClasses { get; set; }
    public bool HasLessonTest { get; set; }
  }

  public class LessonPrerequisiteForm
  {

    public LessonPrerequisiteForm()
    {
      LessonPrerequisites = new List<LessonPrerequisiteViewModel>();
    }
    public int LessonId { get; set; }
    public List<LessonPrerequisiteViewModel> LessonPrerequisites { get; set; }
  }

  public class LessonOutcomeForm
  {
    public LessonOutcomeForm()
    {
      LessonOutcomes = new List<LessonLearningOutcomeViewModel>();
    }
    public int LessonId { get; set; }
    public List<LessonLearningOutcomeViewModel> LessonOutcomes { get; set; }
  }

  public class LessonTopicForm
  {
    public LessonTopicForm()
    {
      LessonTopics = new List<LessonTopicViewModel>();
    }

    public int LessonId { get; set; }
    public List<LessonTopicViewModel> LessonTopics { get; set; }
  }

}
