using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.Attendance.Queries.GetAttendanceDetailForSubjectClassById;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLesson
{
    public class SaveLessonCommand : IRequest<ResponseViewModel>
    {
        public LessonViewModel Vm { get; set; }
    }

    public class SaveLessonCommandHandler : IRequestHandler<SaveLessonCommand, ResponseViewModel>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;
        private readonly ILessonTopicCommandRepository _lessonTopicCommandRepository;
        private readonly ILessonLectureCommandRepository _lessonLectureCommandRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILessonDesignService _lessonDesignService;
        private readonly ILogger<SaveLessonCommandHandler> _logger;

        public SaveLessonCommandHandler(
            ILessonQueryRepository lessonQueryRepository, 
            ILessonCommandRepository lessonCommandRepository, 
            ILessonTopicCommandRepository lessonTopicCommandRepository,
            ILessonLectureCommandRepository lessonLectureCommandRepository,
            ICurrentUserService currentUserService,
            ILessonDesignService lessonDesignService,
            ILogger<SaveLessonCommandHandler> logger)
        {
            this._lessonQueryRepository = lessonQueryRepository;
            this._lessonCommandRepository = lessonCommandRepository;
            this._lessonTopicCommandRepository = lessonTopicCommandRepository;
            this._lessonLectureCommandRepository = lessonLectureCommandRepository;
            this._currentUserService = currentUserService;
            this._lessonDesignService = lessonDesignService;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(SaveLessonCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {

                var lesson = await _lessonQueryRepository.GetById(request.Vm.Id, cancellationToken);

                lesson.Name = request.Vm.LessonDetail.Name;
                lesson.LessonIntroduction = request.Vm.LessonDetail.LessonIntroduction;
                lesson.Duration = request.Vm.LessonDetail.Duration;
                lesson.CompetencyLevel = request.Vm.LessonDetail.CompetencyLevel;
                lesson.TeachingAids = string.Join(",", request.Vm.LessonDetail.TeacherAids);
                lesson.LessonOwnerId = _currentUserService.UserId.Value;
                lesson.GradeId = request.Vm.LessonDetail.GradeId;
                lesson.SubjectId = request.Vm.LessonDetail.SubjectId;
                lesson.TeachingProcess = request.Vm.LessonDetail.TeachingProcess;
                lesson.IsActive = true;
                lesson.UpdatedOn = DateTime.UtcNow;
                lesson.UpdatedById = _currentUserService.UserId.Value;

                //Handle Lesson Classes
                _lessonDesignService.HandleLessonClasses(lesson, request.Vm.LessonDetail);

                //Handle  Lesson Learning outcome
                _lessonDesignService.HandleLessonLearningOutcome(lesson, request.Vm.LessonOutcomeForm);

                //Handle Lesson Prerequisites
                _lessonDesignService.HandleLessonPrerequisites(lesson, request.Vm.LessonPrerequisiteForm);

                foreach (var topic in request.Vm.LessonTopicForm.LessonTopics)
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
                        lessonTopic.Name = request.Vm.LessonDetail.Name;
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

                                await _lessonLectureCommandRepository.UpdateAsync(lecture, cancellationToken);
                            }
                        }


                        await _lessonTopicCommandRepository.UpdateAsync(lessonTopic, cancellationToken);
                    }
                }


                await _lessonCommandRepository.UpdateAsync(lesson, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the lesson. Please try again.";

            }

            return response;
        }
    }
}
