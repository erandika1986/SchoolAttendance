using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLearningOutcome;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonTopicName
{
    public class SaveLessonTopicNameCommand : IRequest<ResponseViewModel>
    {
        public LessonTopicViewModel Vm { get; set; }
    }

    public class SaveLessonTopicNameCommandHandler : IRequestHandler<SaveLessonTopicNameCommand, ResponseViewModel>
    {
        private readonly ILessonTopicCommandRepository _lessonTopicCommandRepository;
        private readonly ILessonTopicQueryRepository _lessonTopicQueryRepository;
        private readonly ILogger<SaveLessonTopicNameCommandHandler> _logger;

        public SaveLessonTopicNameCommandHandler(
            ILessonTopicCommandRepository lessonTopicCommandRepository, 
            ILessonTopicQueryRepository lessonTopicQueryRepository, 
            ILogger<SaveLessonTopicNameCommandHandler> logger)
        {
            this._lessonTopicQueryRepository = lessonTopicQueryRepository;
            this._lessonTopicCommandRepository = lessonTopicCommandRepository;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(SaveLessonTopicNameCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var topic = await _lessonTopicQueryRepository.GetById(request.Vm.Id, cancellationToken);

                topic.Name = request.Vm.Name;

                await _lessonTopicCommandRepository.UpdateAsync(topic, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Topic name successfully saved.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the topic name";
            }


            return response;
        }
    }
}
