using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLearningOutcome;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.CreateNewLessonTopic
{
    public record CreateNewLessonTopicCommand(int lessonId) : IRequest<LessonTopicViewModel>
    {
    }

    public class CreateNewLessonTopicCommandHandler : IRequestHandler<CreateNewLessonTopicCommand, LessonTopicViewModel>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;
        private readonly ILessonDesignService _lessonDesignService;
        private readonly ILogger<CreateNewLessonTopicCommandHandler> _logger;

        public CreateNewLessonTopicCommandHandler(
            ILessonQueryRepository lessonQueryRepository,
            ILessonCommandRepository lessonCommandRepository,
            ILessonDesignService lessonDesignService,
            ILogger<CreateNewLessonTopicCommandHandler> logger)
        {
            this._lessonQueryRepository = lessonQueryRepository;
            this._lessonCommandRepository = lessonCommandRepository;
            this._lessonDesignService = lessonDesignService;
            this._logger = logger;
        }

        public async Task<LessonTopicViewModel> Handle(CreateNewLessonTopicCommand request, CancellationToken cancellationToken)
        {
            var response = new LessonTopicViewModel();

            try
            {
                var lesson = await _lessonQueryRepository
                    .GetById(request.lessonId, cancellationToken);

                var topicCount = lesson.LessonTopics.Count();

                var topicName = string.Format("Topic {0}", topicCount + 1);

                var topic = new LessonTopic()
                {
                    SequenceNo = topicCount + 1,
                    Name = topicName
                };

                lesson.LessonTopics.Add(topic);

                await _lessonCommandRepository.UpdateAsync(lesson, cancellationToken);

                response =  topic.ToVM();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());    
            }

            return response;
        }
    }
}
