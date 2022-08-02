using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SchoolAttendance.WebAPI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class LessonDesignController : ControllerBase
  {
    private readonly ILogger<LessonDesignController> logger;
    private readonly IConfiguration config;
    private readonly ILessonDesignService lessonDesignService;
    private readonly IIdentityService identityService;

    public LessonDesignController(ILogger<LessonDesignController> logger, IConfiguration config, ILessonDesignService lessonDesignService, IIdentityService identityService)
    {
      this.logger = logger;
      this.config = config;
      this.lessonDesignService = lessonDesignService;
      this.identityService = identityService;
    }


    [HttpGet]
    [Route("getLessonDesignDropdownMasterData")]
    public LessonListFilterMasterData GetLessonDesignDropdownMasterData()
    {
      var userName = identityService.GetUserName();

      var response =  lessonDesignService.GetLessonDesignDropdownMasterData(userName);

      return response;
    }

    [HttpPost]
    [Route("createNewLesson")]
    public async Task<LessonViewModel> CreateNewLesson()
    {
      var userName = identityService.GetUserName();

      var response =await  lessonDesignService.CreateNewLesson(userName);

      return response;

    }

    [HttpGet]
    [Route("getLessonById/{id}")]
    public LessonViewModel GetLessonById(int id)
    {
      var response =  lessonDesignService.GetLessonById(id);

      return response;

    }


    [HttpPost]
    [Route("saveLesson")]
    public async Task<ResponseViewModel> SaveLesson(LessonViewModel vm)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.SaveLesson(vm,userName);

      return response;
    }

    [HttpPost]
    [Route("saveLessonDetail")]
    public async Task<ResponseViewModel> SaveLessonDetail(LessonDetailViewModel vm)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.SaveLessonDetail(vm, userName);

      return response;
    }

    [HttpPost]
    [Route("saveLessonPrerequisite")]
    public async Task<ResponseViewModel> SaveLessonPrerequisite(LessonPrerequisiteForm vm)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.SaveLessonPrerequisite(vm, userName);

      return response;
    }

    [HttpPost]
    [Route("saveLessonLearningOutcome")]
    public async Task<ResponseViewModel> SaveLessonLearningOutcome(LessonOutcomeForm vm)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.SaveLessonLearningOutcome(vm, userName);

      return response;
    }

    [HttpPost]
    [Route("createNewLessonTopic")]
    public async Task<LessonTopicViewModel> CreateNewLessonTopic([FromBody] int lessonId)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.CreateNewLessonTopic(lessonId, userName);

      return response;
    }

    [HttpPost]
    [Route("saveLessonTopic")]
    public async Task<LessonTopicViewModel> SaveLessonTopic(LessonTopicViewModel vm)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.SaveLessonTopic(vm, userName);

      return response;
    }

    [HttpPost]
    [Route("createNewLecture")]
    public async Task<LessonLectureViewModel> CreateNewLecture([FromBody] int topicId)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.CreateNewLecture(topicId, userName);

      return response;
    }

    [HttpPost]
    [Route("saveLessonTopicName")]
    public async Task<ResponseViewModel> SaveLessonTopicName(LessonTopicViewModel vm)
    {
      var response = await lessonDesignService.SaveLessonTopicName(vm);

      return response;
    }

    [HttpPost]
    [Route("saveLessonLectureName")]
    public async Task<ResponseViewModel> SaveLessonLectureName(LessonLectureViewModel vm)
    {
      var response = await lessonDesignService.SaveLessonLectureName(vm);

      return response;
    }


    [HttpPost]
    [Route("saveLessonLectureContent")]
    public async Task<LessonLectureViewModel> SaveLessonLectureContent(LessonLectureViewModel vm)
    {
      var response = await lessonDesignService.SaveLessonLectureContent(vm);

      return response;
    }


    [HttpPost]
    [Route("saveLessonLecture")]
    public async Task<LessonLectureViewModel> SaveLessonLecture(LessonLectureViewModel vm)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.SaveLessonLecture(vm, userName);

      return response;
    }


    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    [Route("uploadLessonFile")]
    public async Task<IActionResult> UploadLessonFile()
    {
      var userName = identityService.GetUserName();

      var container = new FileContainerViewModel();

      var request = await Request.ReadFormAsync();

      var id = int.Parse(request["id"]);
      var topicId = int.Parse(request["topicId"]);
      var contentType = int.Parse(request["contentType"]);

      foreach (var file in request.Files)
      {
        container.Files.Add(file);
      }

      var topic = new LessonLectureViewModel()
      {
        Id = id,
        TopicId = topicId,
        ContentType = contentType
      };

      var response = await lessonDesignService.UploadTopicContentFile(topic, request.Files.FirstOrDefault(), userName);

      return Ok(response);
    }


    [HttpPost]
    [Route("saveLessonUnitTest")]
    public async Task<LessonUnitTestViewModel> SaveLessonUnitTest(LessonUnitTestViewModel vm)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.SaveLessonUnitTest(vm, userName);

      return response;
    }

    [HttpPost]
    [Route("saveUnitTestDetail")]
    public async Task<LessonUnitTestViewModel> SaveUnitTestDetail(LessonUnitTestViewModel vm)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.SaveUnitTestDetail(vm, userName);

      return response;
    }

    [HttpPost]
    [Route("saveLessonUnitTestTopic")]
    public async Task<LessonUnitTestTopicViewModel> SaveLessonUnitTestTopic(LessonUnitTestTopicViewModel vm)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.SaveLessonUnitTestTopic(vm, userName);

      return response;
    }

    [HttpPost]
    [Route("copyLesson")]
    public async Task<ResponseViewModel> CopyLesson(int id)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.CopyLesson(id, userName);

      return response;
    }


    [HttpDelete]
    [Route("deleteLesson/{id}")]
    public async Task<ResponseViewModel> DeleteLesson(int id)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.DeleteLesson(id, userName);

      return response;
    }

    [HttpDelete]
    [Route("deleteTopic/{id}")]
    public async Task<ResponseViewModel> DeleteTopic(int id)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.DeleteTopic(id, userName);

      return response;
    }

    [HttpDelete]
    [Route("deleteLecture/{id}")]
    public async Task<ResponseViewModel> DeleteLecture(int id)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.DeleteLecture(id, userName);

      return response;
    }


    [HttpDelete]
    [Route("deleteLectureContent/{id}")]
    public async Task<ResponseViewModel> DeleteLectureContent(int id)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.DeleteLectureContent(id, userName);

      return response;
    }

    [HttpPost]
    [Route("getNotPublishedLesson")]
    public PaginatedItemsViewModel<BasicLessonViewModel> GetNotPublishedLesson(LessonFilter filter)
    {
      var userName = identityService.GetUserName();

      var response =  lessonDesignService.GetNotPublishedLesson(filter, userName);

      return response;
    }

    [HttpPost]
    [Route("publishLesson")]
    public async Task<ResponseViewModel> PublishLesson(int id)
    {
      var userName = identityService.GetUserName();

      var response = await lessonDesignService.PublishLesson(id, userName);

      return response;
    }
  }
}
