using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.CopyLesson;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.CreateNewLecture;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.CreateNewLesson;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.CreateNewLessonTopic;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.DeleteLecture;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.DeleteLectureContent;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.DeleteLesson;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.DeleteTopic;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.PublishLesson;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLesson;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonDetail;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLearningOutcome;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLecture;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLectureContent;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLectureName;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonPrerequisite;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonTopic;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonTopicName;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonUnitTest;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonUnitTestTopic;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveUnitTestDetail;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.UploadTopicContentFile;
using SchoolAttendance.Application.Pipelines.LessonDesign.Queries.GetLessonById;
using SchoolAttendance.Application.Pipelines.LessonDesign.Queries.GetLessonDesignDropdownMasterData;
using SchoolAttendance.Application.Pipelines.LessonDesign.Queries.GetNotPublishedLesson;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
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
        private readonly IMediator _mediator;

        public LessonDesignController(
            ILogger<LessonDesignController> logger,
            IConfiguration config,
            ILessonDesignService lessonDesignService,
            IIdentityService identityService,
            IMediator mediator)
        {
            this.logger = logger;
            this.config = config;
            this.lessonDesignService = lessonDesignService;
            this.identityService = identityService;
            this._mediator = mediator;
        }


        [HttpGet]
        [Route("getLessonDesignDropdownMasterData")]
        public async Task<IActionResult> GetLessonDesignDropdownMasterData()
        {

            var response = await _mediator.Send(new GetLessonDesignDropdownMasterDataQuery());

            return Ok(response);
        }

        [HttpPost]
        [Route("createNewLesson")]
        public async Task<IActionResult> CreateNewLesson()
        {
            var response = await _mediator.Send(new CreateNewLessonCommand());

            return Ok(response);

        }

        [HttpGet]
        [Route("getLessonById/{id}")]
        public async Task<IActionResult> GetLessonById(int id)
        {
            var response = await _mediator.Send(new GetLessonByIdQuery(id));

            return Ok(response);
        }


        [HttpPost]
        [Route("saveLesson")]
        public async Task<IActionResult> SaveLesson(LessonViewModel vm)
        {
            var response = await _mediator.Send(new SaveLessonCommand() { Vm = vm });

            return Ok(response);
        }

        [HttpPost]
        [Route("saveLessonDetail")]
        public async Task<IActionResult> SaveLessonDetail(LessonDetailViewModel vm)
        {
            var response = await _mediator.Send(new SaveLessonDetailCommand() { Vm = vm });

            return Ok(response);
        }

        [HttpPost]
        [Route("saveLessonPrerequisite")]
        public async Task<IActionResult> SaveLessonPrerequisite(LessonPrerequisiteForm vm)
        {
            var response = await _mediator.Send(new SaveLessonPrerequisiteCommand() { Vm = vm });

            return Ok(response);
        }

        [HttpPost]
        [Route("saveLessonLearningOutcome")]
        public async Task<IActionResult> SaveLessonLearningOutcome(LessonOutcomeForm vm)
        {
            var response = await _mediator.Send(new SaveLessonLearningOutcomeCommand() { Vm = vm });

            return Ok(response);
        }

        [HttpPost]
        [Route("createNewLessonTopic")]
        public async Task<IActionResult> CreateNewLessonTopic([FromBody] int lessonId)
        {
            var response = await _mediator.Send(new CreateNewLessonTopicCommand(lessonId));

            return Ok(response);
        }

        [HttpPost]
        [Route("saveLessonTopic")]
        public async Task<IActionResult> SaveLessonTopic(LessonTopicViewModel vm)
        {
            var response = await _mediator.Send(new SaveLessonTopicCommand() { Vm = vm});

            return Ok(response);
        }

        [HttpPost]
        [Route("createNewLecture")]
        public async Task<IActionResult> CreateNewLecture([FromBody] int topicId)
        {
            var response = await _mediator.Send(new CreateNewLectureCommand(topicId));

            return Ok(response);
        }

        [HttpPost]
        [Route("saveLessonTopicName")]
        public async Task<IActionResult> SaveLessonTopicName(LessonTopicViewModel vm)
        {
            var response = await _mediator.Send(new SaveLessonTopicNameCommand() { Vm = vm});

            return Ok(response);
        }

        [HttpPost]
        [Route("saveLessonLectureName")]
        public async Task<IActionResult> SaveLessonLectureName(LessonLectureViewModel vm)
        {
            var response = await _mediator.Send(new SaveLessonLectureNameCommand() { Vm = vm });

            return Ok(response);
        }


        [HttpPost]
        [Route("saveLessonLectureContent")]
        public async Task<IActionResult> SaveLessonLectureContent(LessonLectureViewModel vm)
        {
            var response = await _mediator.Send(new SaveLessonLectureContentCommand() { Vm = vm });

            return Ok(response);
        }


        [HttpPost]
        [Route("saveLessonLecture")]
        public async Task<IActionResult> SaveLessonLecture(LessonLectureViewModel vm)
        {
            var response = await _mediator.Send(new SaveLessonLectureCommand() { Vm = vm });

            return Ok(response);
        }


        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        [Route("uploadLessonFile")]
        public async Task<IActionResult> UploadLessonFile()
        {
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

            var response = await _mediator.Send(new UploadTopicContentFileCommand() 
            { 
                File = request.Files.FirstOrDefault(), 
                Vm = topic 
            });

            return Ok(response);
        }


        [HttpPost]
        [Route("saveLessonUnitTest")]
        public async Task<IActionResult> SaveLessonUnitTest(LessonUnitTestViewModel vm)
        {
            var response = await _mediator.Send(new SaveLessonUnitTestCommand() { Vm = vm });

            return Ok(response);
        }

        [HttpPost]
        [Route("saveUnitTestDetail")]
        public async Task<IActionResult> SaveUnitTestDetail(LessonUnitTestViewModel vm)
        {
            var response = await _mediator.Send(new SaveUnitTestDetailCommand() { Vm = vm });

            return Ok(response);
        }

        [HttpPost]
        [Route("saveLessonUnitTestTopic")]
        public async Task<IActionResult> SaveLessonUnitTestTopic(LessonUnitTestTopicViewModel vm)
        {
            var response = await _mediator.Send(new SaveLessonUnitTestTopicCommand() { Vm = vm });

            return Ok(response);
        }

        [HttpPost]
        [Route("copyLesson")]
        public async Task<IActionResult> CopyLesson(int id)
        {
            var response = await _mediator.Send(new CopyLessonCommand(id));

            return Ok(response);
        }


        [HttpDelete]
        [Route("deleteLesson/{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var response = await _mediator.Send(new DeleteLessonCommand(id));

            return Ok(response);
        }

        [HttpDelete]
        [Route("deleteTopic/{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var response = await _mediator.Send(new DeleteTopicCommand(id));

            return Ok(response);
        }

        [HttpDelete]
        [Route("deleteLecture/{id}")]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            var response = await _mediator.Send(new DeleteLectureCommand(id));

            return Ok(response);
        }


        [HttpDelete]
        [Route("deleteLectureContent/{id}")]
        public async Task<IActionResult> DeleteLectureContent(int id)
        {
            var response = await _mediator.Send(new DeleteLectureContentCommand(id));

            return Ok(response);
        }

        [HttpPost]
        [Route("getNotPublishedLesson")]
        public async Task<IActionResult> GetNotPublishedLesson(LessonFilter filter)
        {
            var response = await _mediator.Send(new GetNotPublishedLessonQuery(filter));

            return Ok(response);
        }

        [HttpPost]
        [Route("publishLesson")]
        public async Task<IActionResult> PublishLesson(int id)
        {
            var response = await _mediator.Send(new PublishLessonCommand(id));

            return Ok(response);
        }
    }
}
