using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
  public class LessonDesignService : ILessonDesignService
  {

    #region Class Member variable

    private readonly ISchoolAttendanceContext db;
    private readonly ILogger<ILessonDesignService> logger;
    private readonly IConfiguration config;
    private readonly ICoreDataService coreDataService;
    private readonly IDropDownService dropDownService;
    private readonly IAzureBlobService azureBlobService;

    #endregion

    #region Constructor

    public LessonDesignService(ISchoolAttendanceContext db, ILogger<ILessonDesignService> logger,
IConfiguration config, ICoreDataService coreDataService, IDropDownService dropDownService, IAzureBlobService azureBlobService)
    {
      this.db = db;
      this.logger = logger;
      this.config = config;
      this.coreDataService = coreDataService;
      this.dropDownService = dropDownService;
      this.azureBlobService = azureBlobService;
    }

    #endregion

    #region Public Methods

    public LessonListFilterMasterData GetLessonDesignDropdownMasterData(string userName)
    {
      var respones = new LessonListFilterMasterData();

      var loggedInUser = coreDataService.GetLoggedInUserByUserName(userName);

      var roles = loggedInUser.UserRoles.Select(x => x.RoleId).ToList();

      if (roles.Contains((int)SystemRole.Admin) || roles.Contains((int)SystemRole.Principle) || roles.Contains((int)SystemRole.VicePrinciple) || roles.Contains((int)SystemRole.DepartmentHead))
      {
        respones.Grades.AddRange(dropDownService.GetAllAcademicLevels());

      }
      else if (roles.Contains((int)SystemRole.LevelHead) || roles.Contains((int)SystemRole.Teacher))
      {
        var levelHeads = new List<DropDownViewModel>();
        if (roles.Contains((int)SystemRole.LevelHead))
        {
          var userLevelHeads = loggedInUser.Grades.ToList();
          if (userLevelHeads.Count > 0)
          {
            levelHeads = userLevelHeads.Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name }).ToList();
          }
        }

        var subjectClasses = loggedInUser.ClassSubjects.Select(x => x.Class.Grade).Where(g => !levelHeads.Any(y => y.Id == g.Id)).Distinct().
              Select(x => new DropDownViewModel() { Id = x.Id, Name = x.Name }).OrderBy(x => x.Id).ToList().Union(levelHeads).ToList();

        respones.Grades.AddRange(subjectClasses);


      }

      if (respones.Grades.Count > 0)
      {
        respones.SelectedGradeId = (int)respones.Grades.FirstOrDefault().Id;
      }

      respones.AcademicYears.AddRange(dropDownService.GetAllAcademicYears());

      foreach (LessonStatus status in (LessonStatus[])Enum.GetValues(typeof(LessonStatus)))
      {
        respones.LessonStatuses.Add(new DropDownViewModel() { Id = (int)status, Name = EnumHelper.GetEnumDescription(status) });
      }

      foreach (TeachingAids aid in (TeachingAids[])Enum.GetValues(typeof(TeachingAids)))
      {
        respones.TeacherAids.Add(new DropDownViewModel() { Id = (int)aid, Name = EnumHelper.GetEnumDescription(aid) });
      }

      respones.CurrentAcademicYear = db.AcademicYears.FirstOrDefault(x => x.IsCurrentYear == true).Id;

      return respones;
    }

    public async Task<LessonViewModel> CreateNewLesson(string userName)
    {

      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);
      var currentAcademicYear = coreDataService.GetCurrentAcademicYear();
      var lessonCount = db.Lessons.Where(x => x.CreatedById == currentUser.Id).Count();

      var lessonNo = string.Format("Lesson {0}", lessonCount + 1);

      var lesson = new Lesson()
      {
        Name = lessonNo,
        AcademicYearId = currentAcademicYear.Id,
        LessonOwnerId = currentUser.Id,
        CreatedOn = DateTime.UtcNow,
        CreatedById = currentUser.Id,
        UpdatedOn = DateTime.UtcNow,
        UpdatedById = currentUser.Id,
        Status = (int)LessonStatus.Design,
        IsActive = true
      };

      db.Lessons.Add(lesson);
      await db.SaveChangesAsync();

      var response = lesson.ToVM();

      return response;
    }

    public LessonViewModel GetLessonById(int id)
    {
      var lesson = db.Lessons.FirstOrDefault(x => x.Id == id);

      return lesson.ToVM();
    }

    public async Task<ResponseViewModel> SaveLessonDetail(LessonDetailViewModel vm, string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var currentUser = coreDataService.GetLoggedInUserByUserName(userName);
        var lesson = db.Lessons.FirstOrDefault(x => x.Id == vm.LessonId);

        lesson.Name = vm.Name;
        lesson.LessonIntroduction = vm.LessonIntroduction;
        lesson.Duration = vm.Duration;
        lesson.CompetencyLevel = vm.CompetencyLevel;
        lesson.TeachingAids = string.Join(",", vm.TeacherAids);
        lesson.LessonOwnerId = currentUser.Id;
        lesson.GradeId = vm.GradeId;
        lesson.SubjectId = vm.SubjectId;
        lesson.TeachingProcess = vm.TeachingProcess;
        lesson.IsActive = true;
        lesson.UpdatedOn = DateTime.UtcNow;
        lesson.UpdatedById = currentUser.Id;
        lesson.HasLessonTest = vm.HasLessonTest;

        //Handle Lesson Classes
        HandleLessonClasses(lesson, vm);

        await db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Lesson detail saved successfully.";
      }
      catch(Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while saving the lesson details. Please try again.";
      }

      return response;
    }

    public async Task<ResponseViewModel> SaveLessonPrerequisite(LessonPrerequisiteForm vm, string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var currentUser = coreDataService.GetLoggedInUserByUserName(userName);
        var lesson = db.Lessons.FirstOrDefault(x => x.Id == vm.LessonId);

        HandleLessonPrerequisites(lesson, vm);

        await db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Lesson Prerequisites saved successfully.";
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while saving the lesson Prerequisites. Please try again.";
      }

      return response;
    }

    public async Task<ResponseViewModel> SaveLessonLearningOutcome(LessonOutcomeForm vm,string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var currentUser = coreDataService.GetLoggedInUserByUserName(userName);
        var lesson = db.Lessons.FirstOrDefault(x => x.Id == vm.LessonId);

        HandleLessonLearningOutcome(lesson, vm);

        await db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Lesson Learning Outcomes saved successfully.";
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while saving the lesson learning outcomes. Please try again.";
      }

      return response;
    }

    public async Task<LessonTopicViewModel> CreateNewLessonTopic(int lessonId,string userName)
    {
      var lesson = db.Lessons.FirstOrDefault(x => x.Id == lessonId);

      var topicCount = lesson.LessonTopics.Count();

      var topicName = string.Format("Topic {0}", topicCount + 1);

      var topic = new LessonTopic()
      {
        SequenceNo = topicCount+1,
        Name= topicName
      };

      lesson.LessonTopics.Add(topic);

      await db.SaveChangesAsync();

      return topic.ToVM();

    }

    public async Task<ResponseViewModel> SaveLessonTopicName(LessonTopicViewModel vm)
    {
      var response = new ResponseViewModel();

      try
      {
        var topic = db.LessonTopics.FirstOrDefault(x => x.Id == vm.Id);

        topic.Name = vm.Name;

        db.LessonTopics.Update(topic);

        await db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Topic name successfully saved.";
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while saving the topic name";
      }


      return response;
    }

    public async Task<LessonLectureViewModel> CreateNewLecture(int topicId,string userName)
    {
      var topic = db.LessonTopics.FirstOrDefault(x => x.Id == topicId);

      var lectureName = string.Format("Lecture {0}", topic.LessonLectures.Count + 1);

      var lecture = new LessonLecture()
      {
        Name = lectureName,
      };

      topic.LessonLectures.Add(lecture);

      await db.SaveChangesAsync();

      return lecture.ToVM();
    }

    public async Task<ResponseViewModel> SaveLessonLectureName(LessonLectureViewModel vm)
    {
      var response = new ResponseViewModel();
      try
      {
        var lecture = db.LessonLectures.FirstOrDefault(x => x.Id == vm.Id);

        lecture.Name = vm.Name;

        db.LessonLectures.Update(lecture);

        await db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Lecture name successfully saved.";
      }
      catch(Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while saving the lecture name";
      }


      return response;
    }

    public async Task<LessonLectureViewModel> SaveLessonLectureContent(LessonLectureViewModel vm)
    {
      try
      {
        var lecture = db.LessonLectures.FirstOrDefault(x => x.Id == vm.Id);

        lecture.LectureContentTypeId = vm.ContentType;

        lecture.LectureContentTypeId = vm.ContentType;

        if (vm.ContentType == (int)LectureContentType.Youtube)
        {
          lecture.LectureContent = vm.Content.Replace("watch?v=", "embed/");
        }
        else
        {
          lecture.LectureContent = vm.Content;
        }

        db.LessonLectures.Update(lecture);

        await db.SaveChangesAsync();

        vm.Content = lecture.LectureContent;
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
      }


      return vm;
    }

    public async Task<ResponseViewModel> SaveLesson(LessonViewModel vm, string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var currentUser = coreDataService.GetLoggedInUserByUserName(userName);
        var lesson = db.Lessons.FirstOrDefault(x => x.Id == vm.Id);

        lesson.Name = vm.LessonDetail.Name;
        lesson.LessonIntroduction = vm.LessonDetail.LessonIntroduction;
        lesson.Duration = vm.LessonDetail.Duration;
        lesson.CompetencyLevel = vm.LessonDetail.CompetencyLevel;
        lesson.TeachingAids = string.Join(",", vm.LessonDetail.TeacherAids);
        lesson.LessonOwnerId = currentUser.Id;
        lesson.GradeId = vm.LessonDetail.GradeId;
        lesson.SubjectId = vm.LessonDetail.SubjectId;
        lesson.TeachingProcess = vm.LessonDetail.TeachingProcess;
        lesson.IsActive = true;
        lesson.UpdatedOn = DateTime.UtcNow;
        lesson.UpdatedById = currentUser.Id;

        //Handle Lesson Classes
        HandleLessonClasses(lesson, vm.LessonDetail);

        //Handle  Lesson Learning outcome
        HandleLessonLearningOutcome(lesson, vm.LessonOutcomeForm);

        //Handle Lesson Prerequisites
        HandleLessonPrerequisites(lesson, vm.LessonPrerequisiteForm);

        foreach (var topic in vm.LessonTopicForm.LessonTopics)
        {
          var lessonTopic = lesson.LessonTopics.FirstOrDefault(t => t.Id == topic.Id);

          if (lessonTopic == null)
          {
            lessonTopic = new LessonTopic()
            {
              Name = $"Topic {lesson.LessonTopics.Count() + 1}",
              SequenceNo = topic.SequenceNo
            };

            lessonTopic.LessonLectures = new HashSet<LessonLecture>();

            foreach (var item in topic.LessonLectures)
            {
              var lecture = new LessonLecture()
              {
                Name = $"New Lecture {topic.LessonLectures.Count + 1}",
                LectureContentTypeId = item.ContentType,
                LectureContent = item.Content
              };

              lessonTopic.LessonLectures.Add(lecture);
            }

            lesson.LessonTopics.Add(lessonTopic);
          }
          else
          {
            lessonTopic.Name = vm.LessonDetail.Name;
            lessonTopic.SequenceNo = topic.SequenceNo;

            foreach (var item in topic.LessonLectures)
            {

              var lecture = lessonTopic.LessonLectures.FirstOrDefault(x => x.Id == item.Id);

              if (lecture == null)
              {
                lecture = new LessonLecture()
                {
                  Name = $"New Lecture {lessonTopic.LessonLectures.Count + 1}",
                  LectureContentTypeId = item.ContentType,
                  LectureContent = item.Content
                };

                lessonTopic.LessonLectures.Add(lecture);
              }
              else
              {
                lecture.Name = item.Name;
                lecture.LectureContentTypeId = item.ContentType;
                lecture.LectureContent = item.Content;

                db.LessonLectures.Update(lecture);
              }
            }


            db.LessonTopics.Update(lessonTopic);
          }
        }


        await db.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while saving the lesson. Please try again.";

      }

      return response;
    }

    public async Task<LessonTopicViewModel> SaveLessonTopic(LessonTopicViewModel vm, string userName)
    {
      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

      var lessonTopic = db.LessonTopics.FirstOrDefault(x => x.Id == vm.Id);

      if (lessonTopic == null)
      {
        var topicCount = db.LessonTopics.Where(x => x.LessonId == vm.LessonId).Count();

        lessonTopic = new LessonTopic()
        {
          LessonId = vm.LessonId,
          Name = $"Topic {topicCount + 1}",
          SequenceNo = topicCount + 1
        };

        db.LessonTopics.Add(lessonTopic);
      }
      else
      {
        lessonTopic.Name = vm.Name;
        lessonTopic.SequenceNo = vm.SequenceNo;

        db.LessonTopics.Update(lessonTopic);

      }

      await db.SaveChangesAsync();

      var response = lessonTopic.ToVM();

      return response;
    }

    public async Task<LessonLectureViewModel> SaveLessonLecture(LessonLectureViewModel vm, string userName)
    {
      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

      var lecture = db.LessonLectures.FirstOrDefault(x => x.Id == vm.Id);

      lecture.LectureContentTypeId = vm.ContentType;

      if(vm.ContentType== (int)LectureContentType.Youtube)
      {
        lecture.LectureContent = vm.Content.Replace("watch?v=", "embed/");
      }
      else
      {
        lecture.LectureContent = vm.Content;
      }


      db.LessonLectures.Update(lecture);

      await db.SaveChangesAsync();

      var response = lecture.ToVM();

      return response;
    }

    public async Task<LessonLectureViewModel> UploadTopicContentFile(LessonLectureViewModel vm, IFormFile file, string userName)
    {
      try
      {
        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        FileInfo fi = new FileInfo(fileName);
        string fileURL = await azureBlobService.UploadAsync(file.OpenReadStream(), fileName, file.ContentType);

        var record = db.LessonLectures.FirstOrDefault(x => x.Id == vm.Id);
        record.LectureContent = fileURL;
        record.LectureContentTypeId = vm.ContentType;
        record.Mimetype = FileHelper.GetMimeType(fi.Extension);

        db.LessonLectures.Update(record);

        await db.SaveChangesAsync();

        vm.Content = fileURL;
        vm.MimeType = record.Mimetype;
      }
      catch (Exception ex)
      {
        //Log error 
      }


      return vm;
    }

    public async Task<LessonUnitTestViewModel> SaveLessonUnitTest(LessonUnitTestViewModel vm, string userName)
    {
      var response = new LessonUnitTestViewModel();

      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

      var lessonUnitTest = db.LessonUnitTests.FirstOrDefault(x => x.Id == vm.Id);

      if (lessonUnitTest == null)
      {
        lessonUnitTest = new LessonUnitTest()
        {
          LessonId = vm.LessonId,
          Name = vm.Name,
          StudentGuide = vm.StudentGuide,
          LessonUnitTestTopics = new HashSet<LessonUnitTestTopic>()
        };

        AddNewLessonUnitTestTopics(vm.Topics, lessonUnitTest, currentUser);
      }
      else
      {
        lessonUnitTest.Name = vm.Name;
        lessonUnitTest.StudentGuide = vm.StudentGuide;

        var savedTopics = lessonUnitTest.LessonUnitTestTopics.ToList();

        //Add Newly Added Topics
        var newlyAddedTopics = vm.Topics.Where(x => !savedTopics.Any(t => t.Id == x.Id)).ToList();

        AddNewLessonUnitTestTopics(newlyAddedTopics, lessonUnitTest, currentUser);

        //Updated ExistingTopics
        var updatedTopics = vm.Topics.Where(x => savedTopics.Any(t => t.Id == x.Id)).ToList();

        UpdatedLessonUnitTest(updatedTopics, lessonUnitTest, currentUser);

        //Delete deleted topics
        var deletedTopics = savedTopics.Where(x => !vm.Topics.Any(t => t.Id == x.Id)).ToList();

        DeleteLessonUnitTestTopics(deletedTopics);
      }

      await db.SaveChangesAsync();

      response = lessonUnitTest.ToVM();

      return response;
    }

    public async Task<LessonUnitTestViewModel> SaveUnitTestDetail(LessonUnitTestViewModel vm, string userName)
    {
      var response = new LessonUnitTestViewModel();

      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

      var lessonUnitTest = db.LessonUnitTests.FirstOrDefault(x => x.Id == vm.Id);

      if (lessonUnitTest == null)
      {
        lessonUnitTest = new LessonUnitTest()
        {
          LessonId = vm.LessonId,
          Name = vm.Name,
          StudentGuide = vm.StudentGuide,
          LessonUnitTestTopics = new HashSet<LessonUnitTestTopic>()
        };

        db.LessonUnitTests.Add(lessonUnitTest);

      }
      else
      {
        lessonUnitTest.Name = vm.Name;
        lessonUnitTest.StudentGuide = vm.StudentGuide;

        db.LessonUnitTests.Update(lessonUnitTest);
      }

      await db.SaveChangesAsync();

      response = lessonUnitTest.ToVM();

      return response;
    }

    public async Task<LessonUnitTestTopicViewModel> SaveLessonUnitTestTopic(LessonUnitTestTopicViewModel vm,string userName)
    {

      try
      {
        var unitTestTopic = db.LessonUnitTestTopics.FirstOrDefault(x => x.Id == vm.Id);

        var unitTest = db.LessonUnitTests.FirstOrDefault(x => x.Id == vm.LessonUnitTestId);

        if(unitTestTopic==null)
        {
          unitTestTopic = new LessonUnitTestTopic()
          {
            LessonUnitTestId = vm.LessonUnitTestId,
            Name = $"Section {unitTest.LessonUnitTestTopics.Count+1}",
            Instruction = string.Empty,
            QuestionTypeId = vm.QuestionTypeId
          };

          db.LessonUnitTestTopics.Add(unitTestTopic);
        }
        else
        {
          unitTestTopic.Name = vm.Name;
          unitTestTopic.Instruction = vm.Instruction;
          unitTestTopic.QuestionTypeId = vm.QuestionTypeId;

          db.LessonUnitTestTopics.Update(unitTestTopic);
        }
        vm.Id = unitTestTopic.Id;
        vm.Name = unitTestTopic.Name;

        await db.SaveChangesAsync();
      }
      catch(Exception ex)
      {
        logger.LogError(ex.ToString());
      }



      return vm;
    }

    public async Task<ResponseViewModel> CopyLesson(int id, string userName)
    {
      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

      throw new NotImplementedException();
    }

    public async Task<ResponseViewModel> DeleteLesson(int id, string userName)
    {
      var response = new ResponseViewModel();

      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

      var lesson = db.Lessons.FirstOrDefault(x => x.Id == id);

      lesson.IsActive = false;
      lesson.UpdatedOn = DateTime.UtcNow;
      lesson.UpdatedById = currentUser.Id;

      db.Lessons.Update(lesson);

      await db.SaveChangesAsync();

      response.IsSuccess = true;
      response.Message = "Lesson has been deleted successfully";

      return response;
    }

    public async Task<ResponseViewModel> DeleteTopic(int id,string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var topic = db.LessonTopics.FirstOrDefault(x => x.Id == id);

        foreach (var item in topic.LessonLectures)
        {
          db.LessonLectures.Remove(item);
        }

        db.LessonTopics.Remove(topic);

        await db.SaveChangesAsync();
        
        response.IsSuccess = true;
        response.Message = "Topic has deleted successfully.";
      }
      catch(Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while deleting the topic.";
      }

      return response;
    }

    public async Task<ResponseViewModel> DeleteLecture(int id, string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var lecture = db.LessonLectures.FirstOrDefault(x => x.Id == id);

        db.LessonLectures.Remove(lecture);

        await db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Lecture has deleted successfully.";
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while deleting the lecture.";
      }

      return response;
    }

    public async Task<ResponseViewModel> DeleteLectureContent(int id,string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var lecture = db.LessonLectures.FirstOrDefault(x => x.Id == id);
        lecture.LectureContentTypeId = (int?)null;
        lecture.LectureContent = "";
        lecture.Mimetype = "";

        db.LessonLectures.Update(lecture);

        await db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Lecture content has been deleted successfully.";
      }
      catch(Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while clearing lecture content.";
      }

      return response;
    }
    public PaginatedItemsViewModel<BasicLessonViewModel> GetNotPublishedLesson(LessonFilter filter, string userName)
    {
      int totalRecordCount = 0;
      double totalPages = 0;
      int totalPageCount = 0;
      var vms = new List<BasicLessonViewModel>();

      var currentUser = coreDataService.GetLoggedInUserByUserName(userName);

      var lessons = db.Lessons.Where(l => l.LessonOwnerId == currentUser.Id).OrderByDescending(x => x.CreatedOn);

      if (filter.AcademicYear > 0)
      {
        lessons = lessons.Where(l => l.AcademicYearId == filter.AcademicYear).OrderByDescending(x => x.CreatedOn);
      }

      if (filter.GradeId > 0)
      {
        lessons = lessons.Where(l => l.GradeId == filter.GradeId).OrderByDescending(x => x.CreatedOn);
      }

      if (filter.SubjectId > 0)
      {
        lessons = lessons.Where(l => l.SubjectId == filter.SubjectId).OrderByDescending(x => x.CreatedOn);
      }

      if (!string.IsNullOrEmpty(filter.SearchText))
      {
        lessons = lessons.Where(x => x.Name.Contains(filter.SearchText)).OrderByDescending(x => x.CreatedOn);
      }

      totalRecordCount = lessons.Count();
      totalPages = (double)totalRecordCount / filter.PageSize;
      totalPageCount = (int)Math.Ceiling(totalPages);

      var lessonList = lessons.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize).ToList();

      foreach (var item in lessonList)
      {
        var vm = new BasicLessonViewModel();

        if (item.AcademicYearId.HasValue)
        {
          vm.AcademicYear = item.AcademicYearId.Value;
        }

        if (item.GradeId.HasValue)
        {
          vm.GradeName = item.Grade.Name;
        }

        if (item.SubjectId.HasValue)
        {
          vm.Subject = item.Subject.Name;
        }

        vm.Id = item.Id;
        vm.Name = item.Name;
        vm.Owner = item.LessonOwner.FullName;
        vm.Status = EnumHelper.GetEnumDescription((LessonStatus)item.Status);
        vm.CreatedOn = item.CreatedOn.ToString("MMM/dd/yyyy");

        vms.Add(vm);
      }

      var container = new PaginatedItemsViewModel<BasicLessonViewModel>(filter.CurrentPage, filter.PageSize, totalPageCount, totalRecordCount, vms);

      return container;
    }

    public async Task<ResponseViewModel> PublishLesson(int id, string userName)
    {
      throw new NotImplementedException();
    }


    #endregion

    #region Private Methods

    private void HandleLessonClasses(Lesson lesson, LessonDetailViewModel vm)
    {
      var savedAssinedClasses = lesson.LessonAssignedClasses.ToList();

      var newAssignedClasses = vm.AssignedClasses.Where(x => !savedAssinedClasses.Any(s => s.ClassId == x)).ToList();

      newAssignedClasses.ForEach(cl =>
      {

        lesson.LessonAssignedClasses.Add(new LessonAssignedClass() { ClassId = cl });
      });

      var deletedClasses = savedAssinedClasses.Where(x => !vm.AssignedClasses.Any(a => a == x.ClassId)).ToList();

      foreach (var item in deletedClasses)
      {
        db.LessonAssignedClasses.Remove(item);
      }
    }

    private void HandleLessonLearningOutcome(Lesson lesson, LessonOutcomeForm vm)
    {
      var savedLearningOutcomes = lesson.LessonLearningOutcomes.ToList();

      var newLearningOutcomes = vm.LessonOutcomes.Where(x => !savedLearningOutcomes.Any(s => s.Id == x.Id)).ToList();

      newLearningOutcomes.ForEach(lo =>
      {

        lesson.LessonLearningOutcomes.Add(new LessonLearningOutcome()
        {
          LearningOutcome = lo.LessonOutcome,
        });
      });

      var deletedLearningOutcome = savedLearningOutcomes.Where(x => !vm.LessonOutcomes.Any(a => a.Id == x.Id)).ToList();

      foreach (var item in deletedLearningOutcome)
      {
        db.LessonLearningOutcomes.Remove(item);
      }
    }

    private void HandleLessonPrerequisites(Lesson lesson, LessonPrerequisiteForm vm)
    {
      var savedLessonPrerequisites = lesson.LessonPrerequisites.ToList();

      var newLessonPrerequisites = vm.LessonPrerequisites.Where(x => !savedLessonPrerequisites.Any(s => s.Id == x.Id)).ToList();

      newLessonPrerequisites.ForEach(lo =>
      {

        lesson.LessonPrerequisites.Add(new LessonPrerequisite()
        {
          Prerequisite = lo.Prerequisite,
        });
      });

      var deletedLessonPrerequisites = savedLessonPrerequisites.Where(x => !vm.LessonPrerequisites.Any(a => a.Id == x.Id)).ToList();

      foreach (var item in deletedLessonPrerequisites)
      {
        db.LessonPrerequisites.Remove(item);
      }
    }

    private void AddNewLessonUnitTestTopics(List<LessonUnitTestTopicViewModel> topics, LessonUnitTest lessonUnitTest, User currentUser)
    {
      foreach (var topic in topics)
      {
        var unitTestTopic = new LessonUnitTestTopic()
        {
          Instruction = topic.Instruction,
          Name = topic.Name,
          QuestionTypeId = topic.QuestionTypeId,
          LessonUnitTestTopicQuestions = new HashSet<LessonUnitTestTopicQuestion>()
        };

        AddNewLessonTopicQuestions(topic.Questions, unitTestTopic, currentUser);

        lessonUnitTest.LessonUnitTestTopics.Add(unitTestTopic);
      }
    }

    private void UpdatedLessonUnitTest(List<LessonUnitTestTopicViewModel> topics, LessonUnitTest lessonUnitTest, User currentUser)
    {
      foreach (var topic in topics)
      {
        var unitTestTopic = lessonUnitTest.LessonUnitTestTopics.FirstOrDefault(x => x.Id == topic.Id);
        unitTestTopic.Instruction = topic.Instruction;
        unitTestTopic.Name = topic.Name;
        unitTestTopic.QuestionTypeId = topic.QuestionTypeId;

        foreach (var question in topic.Questions)
        {
          var savedTopicQuestion = unitTestTopic.LessonUnitTestTopicQuestions.ToList();

          //Add Newly Added Questions
          var newlyAddedTopicQuestions = topic.Questions.Where(x => !savedTopicQuestion.Any(tq => tq.Id == x.Id)).ToList();
          AddNewLessonTopicQuestions(newlyAddedTopicQuestions, unitTestTopic, currentUser);

          //Update existing questions
          var updatedTopicQuestions = topic.Questions.Where(x => savedTopicQuestion.Any(tq => tq.Id == x.Id)).ToList();
          UpdateLessonTopicQuestions(updatedTopicQuestions, unitTestTopic, currentUser);

          //Delete deleted LessonUnitTest Questions
          var deletedTopicQuestions = savedTopicQuestion.Where(x => !topic.Questions.Any(q => q.Id == x.Id)).ToList();
          DeleteLessonTopicQuestions(deletedTopicQuestions);
        }


        db.LessonUnitTestTopics.Update(unitTestTopic);
      }
    }

    private void DeleteLessonUnitTestTopics(List<LessonUnitTestTopic> deletedTopics)
    {
      foreach (var deletedTopic in deletedTopics)
      {
        DeleteLessonTopicQuestions(deletedTopic.LessonUnitTestTopicQuestions.ToList());

        db.LessonUnitTestTopics.Remove(deletedTopic);
      }
    }

    private void AddNewLessonTopicQuestions(List<LessonUnitTestTopicQuestionViewModel> questions, LessonUnitTestTopic lessonUnitTestTopic, User currentUser)
    {
      foreach (var q in questions)
      {
        var topicQuestion = new LessonUnitTestTopicQuestion()
        {
          SequenceNo = q.SequenceNo,
          Score = q.Score
        };

        if (lessonUnitTestTopic.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.MCQ)
        {
          topicQuestion.Question = new Question()
          {
            AcademicYearId = q.AcademicYearId,
            GradeId = q.GradeId,
            SubjectId = q.SubjectId,
            OwnerId = currentUser.Id,
            Question1 = q.MCQQuestion.Question,
            QuestionRt = q.MCQQuestion.QuestionRT,
            QuestionTypeId = lessonUnitTestTopic.QuestionTypeId
          };

          topicQuestion.Question.QuestionMcqteacherAnswers = new HashSet<QuestionMcqteacherAnswer>();

          foreach (var item in q.MCQQuestion.TeacherAnswers)
          {
            var teacherMCQAnswer = new QuestionMcqteacherAnswer()
            {
              AnswerText = item.AnswerText,
              AnswerTextRt = item.AnswerTextRT,
              SequenceNo = item.SequenceNo,
              IsCorrectAnswer = item.IsCorrectAnswer
            };

            topicQuestion.Question.QuestionMcqteacherAnswers.Add(teacherMCQAnswer);
          }
        }
        else if (lessonUnitTestTopic.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.OpenEnded)
        {
          topicQuestion.Question = new Question()
          {
            AcademicYearId = q.AcademicYearId,
            GradeId = q.GradeId,
            SubjectId = q.SubjectId,
            OwnerId = currentUser.Id,
            Question1 = q.OpenEndedQuestion.Question,
            QuestionRt = q.OpenEndedQuestion.QuestionRT,
            QuestionTypeId = lessonUnitTestTopic.QuestionTypeId
          };

          topicQuestion.Question.QuestionOpenEndedTeacherAnswers = new HashSet<QuestionOpenEndedTeacherAnswer>();

          foreach (var item in q.OpenEndedQuestion.TeacherAnswers)
          {
            var teacherOpenEndedAnswer = new QuestionOpenEndedTeacherAnswer()
            {
              AnswerText = item.AnswerText,
              AnswerTextRt = item.AnswerTextRT
            };

            topicQuestion.Question.QuestionOpenEndedTeacherAnswers.Add(teacherOpenEndedAnswer);
          }
        }

        lessonUnitTestTopic.LessonUnitTestTopicQuestions.Add(topicQuestion);

      }
    }

    private void UpdateLessonTopicQuestions(List<LessonUnitTestTopicQuestionViewModel> questions, LessonUnitTestTopic lessonUnitTestTopic, User currentUser)
    {
      var savedQuestions = lessonUnitTestTopic.LessonUnitTestTopicQuestions.ToList();

      //Add new questions to the topic
      var newQuestions = questions.Where(q => !savedQuestions.Any(sq => sq.Id == q.Id)).ToList();
      AddNewLessonTopicQuestions(newQuestions, lessonUnitTestTopic, currentUser);

      //Update existingQuestions
      var updatedQuestions = questions.Where(q => savedQuestions.Any(sq => sq.Id == q.Id)).ToList();

      foreach (var item in updatedQuestions)
      {
        var lessonUnitTestTopicQuestion = lessonUnitTestTopic.LessonUnitTestTopicQuestions.FirstOrDefault(x => x.Id == item.Id);

        lessonUnitTestTopicQuestion.SequenceNo = item.SequenceNo;
        lessonUnitTestTopicQuestion.Score = item.Score;



        if (lessonUnitTestTopic.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.MCQ)
        {
          lessonUnitTestTopicQuestion.Question.Question1 = item.MCQQuestion.Question;
          lessonUnitTestTopicQuestion.Question.QuestionRt = item.MCQQuestion.QuestionRT;

          var savedAnswers = lessonUnitTestTopicQuestion.Question.QuestionMcqteacherAnswers.ToList();

          //Add newly added answers
          var newAnswers = item.MCQQuestion.TeacherAnswers.Where(a => !savedAnswers.Any(sa => sa.Id == a.Id)).ToList();
          foreach (var newAnswer in newAnswers)
          {
            var teacherMCQAnswer = new QuestionMcqteacherAnswer()
            {
              AnswerText = newAnswer.AnswerText,
              AnswerTextRt = newAnswer.AnswerTextRT,
              SequenceNo = newAnswer.SequenceNo,
              IsCorrectAnswer = newAnswer.IsCorrectAnswer
            };

            lessonUnitTestTopicQuestion.Question.QuestionMcqteacherAnswers.Add(teacherMCQAnswer);
          }

          //Update modifed answers
          var updatedAnswers = item.MCQQuestion.TeacherAnswers.Where(a => savedAnswers.Any(sa => sa.Id == a.Id)).ToList();
          foreach (var updatedAnswer in updatedAnswers)
          {
            var teacherMCQAnswer = lessonUnitTestTopicQuestion.Question.QuestionMcqteacherAnswers.FirstOrDefault(x => x.Id == updatedAnswer.Id);

            teacherMCQAnswer.AnswerText = updatedAnswer.AnswerText;
            teacherMCQAnswer.AnswerTextRt = updatedAnswer.AnswerTextRT;
            teacherMCQAnswer.SequenceNo = updatedAnswer.SequenceNo;
            teacherMCQAnswer.IsCorrectAnswer = updatedAnswer.IsCorrectAnswer;

            db.QuestionMcqteacherAnswers.Update(teacherMCQAnswer);
          }

          //Delete deleted answers
          var deletedAnswers = savedAnswers.Where(da => !item.MCQQuestion.TeacherAnswers.Any(ta => ta.Id == da.Id)).ToList();

          foreach (var deleteAnswer in deletedAnswers)
          {
            db.QuestionMcqteacherAnswers.Remove(deleteAnswer);
          }

        }
        else if (lessonUnitTestTopic.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.OpenEnded)
        {
          lessonUnitTestTopicQuestion.Question.Question1 = item.OpenEndedQuestion.Question;
          lessonUnitTestTopicQuestion.Question.QuestionRt = item.OpenEndedQuestion.QuestionRT;

          var savedAnswers = lessonUnitTestTopicQuestion.Question.QuestionOpenEndedTeacherAnswers.ToList();

          //Add newly added answers
          var newAnswers = item.OpenEndedQuestion.TeacherAnswers.Where(a => !savedAnswers.Any(sa => sa.Id == a.Id)).ToList();
          foreach (var newAnswer in newAnswers)
          {
            var teacherOpenEndedAnswer = new QuestionOpenEndedTeacherAnswer()
            {
              AnswerText = newAnswer.AnswerText,
              AnswerTextRt = newAnswer.AnswerTextRT,
            };

            lessonUnitTestTopicQuestion.Question.QuestionOpenEndedTeacherAnswers.Add(teacherOpenEndedAnswer);
          }

          //Update modifed answers
          var updatedAnswers = item.OpenEndedQuestion.TeacherAnswers.Where(a => savedAnswers.Any(sa => sa.Id == a.Id)).ToList();
          foreach (var updatedAnswer in updatedAnswers)
          {
            var teacherOpenEndedAnswer = lessonUnitTestTopicQuestion.Question.QuestionOpenEndedTeacherAnswers.FirstOrDefault(x => x.Id == updatedAnswer.Id);

            teacherOpenEndedAnswer.AnswerText = updatedAnswer.AnswerText;
            teacherOpenEndedAnswer.AnswerTextRt = updatedAnswer.AnswerTextRT;

            db.QuestionOpenEndedTeacherAnswers.Update(teacherOpenEndedAnswer);
          }

          //Delete deleted answers
          var deletedAnswers = savedAnswers.Where(da => !item.OpenEndedQuestion.TeacherAnswers.Any(ta => ta.Id == da.Id)).ToList();

          foreach (var deleteAnswer in deletedAnswers)
          {
            db.QuestionOpenEndedTeacherAnswers.Remove(deleteAnswer);
          }
        }

        db.LessonUnitTestTopicQuestions.Update(lessonUnitTestTopicQuestion);

      }


      //Delete deleted Question from to topic
      var deletedQuestions = savedQuestions.Where(sq => !questions.Any(q => q.Id == sq.Id)).ToList();
      DeleteLessonTopicQuestions(deletedQuestions);
    }

    private void DeleteLessonTopicQuestions(List<LessonUnitTestTopicQuestion> topicQuestions)
    {
      foreach (var topicQuestion in topicQuestions)
      {
        if (topicQuestion.Question.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.MCQ)
        {
          foreach (var teacherAnswer in topicQuestion.Question.QuestionMcqteacherAnswers)
          {
            db.QuestionMcqteacherAnswers.Remove(teacherAnswer);
          }
        }
        else if (topicQuestion.Question.QuestionTypeId == (int)SchoolAttendance.Domain.Enums.QuestionType.OpenEnded)
        {
          foreach (var teacherAnswer in topicQuestion.Question.QuestionOpenEndedTeacherAnswers)
          {
            db.QuestionOpenEndedTeacherAnswers.Remove(teacherAnswer);
          }
        }
        db.Questions.Remove(topicQuestion.Question);
        db.LessonUnitTestTopicQuestions.Remove(topicQuestion);
      }
    }

    #endregion
  }
}
