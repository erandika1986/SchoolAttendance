using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonTopic
{
    public class SaveLessonTopicCommand : IRequest<LessonTopicViewModel>
    {
        public LessonTopicViewModel Vm { get; set; }
    }

    public class SaveLessonTopicCommandHandler : IRequestHandler<SaveLessonTopicCommand, LessonTopicViewModel>
    {
        private readonly ILessonTopicQueryRepository _lessonTopicQueryRepository;
        private readonly ILessonTopicCommandRepository _lessonTopicCommandRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<SaveLessonTopicCommandHandler> _logger;

        public SaveLessonTopicCommandHandler(
            ILessonTopicQueryRepository lessonTopicQueryRepository,
            ILessonTopicCommandRepository lessonTopicCommandRepository,
            ICurrentUserService currentUserService, 
            ILogger<SaveLessonTopicCommandHandler> logger)
        {
            this._lessonTopicQueryRepository = lessonTopicQueryRepository;
            this._lessonTopicCommandRepository = lessonTopicCommandRepository;
            this._currentUserService = currentUserService;
            this._logger = logger;
        }


        public async Task<LessonTopicViewModel> Handle(SaveLessonTopicCommand request, CancellationToken cancellationToken)
        {
            var lessonTopic = await _lessonTopicQueryRepository.GetById(request.Vm.Id, cancellationToken);

            if (lessonTopic == null)
            {
                var topicCount = (await _lessonTopicQueryRepository
                    .Query(x => x.LessonId == request.Vm.LessonId)).Count();

                lessonTopic = new LessonTopic()
                {
                    LessonId = request.Vm.LessonId,
                    Name = $"Topic {topicCount + 1}",
                    SequenceNo = topicCount + 1
                };

                await _lessonTopicCommandRepository
                    .AddAsync(lessonTopic, cancellationToken);
            }
            else
            {
                lessonTopic.Name = request.Vm.Name;
                lessonTopic.SequenceNo = request.Vm.SequenceNo;

                await _lessonTopicCommandRepository
                    .UpdateAsync(lessonTopic, cancellationToken);
            }

            var response = lessonTopic.ToVM();

            return response;
        }
    }
}
