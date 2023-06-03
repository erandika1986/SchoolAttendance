using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.DeleteTopic
{
    public record DeleteTopicCommand(int id) : IRequest<ResponseViewModel>
    {
    }

    public class DeleteTopicCommandHandler : IRequestHandler<DeleteTopicCommand, ResponseViewModel>
    {
        private readonly ILessonTopicQueryRepository _lessonTopicQueryRepository;
        private readonly ILessonTopicCommandRepository _lessonTopicCommandRepository;
        private readonly ILessonLectureCommandRepository _lessonLectureCommandRepository;
        private readonly ILogger<DeleteTopicCommandHandler> _logger;

        public DeleteTopicCommandHandler(
            ILessonTopicQueryRepository lessonTopicQueryRepository, 
            ILessonTopicCommandRepository lessonTopicCommandRepository,
            ILessonLectureCommandRepository lessonLectureCommandRepository,
            ILogger<DeleteTopicCommandHandler> logger)
        {
            this._lessonTopicQueryRepository = lessonTopicQueryRepository;
            this._lessonTopicCommandRepository = lessonTopicCommandRepository;
            this._lessonLectureCommandRepository = lessonLectureCommandRepository;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var topic = await _lessonTopicQueryRepository.GetById(request.id, cancellationToken);

                foreach (var item in topic.LessonLectures)
                {
                    await _lessonLectureCommandRepository.DeleteAsync(item, cancellationToken);
                }

                await _lessonTopicCommandRepository.DeleteAsync(topic, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Topic has deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting the topic.";
            }

            return response;
        }
    }
}
