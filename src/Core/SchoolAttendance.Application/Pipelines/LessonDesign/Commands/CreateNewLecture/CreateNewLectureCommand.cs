using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.CreateNewLecture
{
    public record CreateNewLectureCommand(int topicId) : IRequest<LessonLectureViewModel>
    {
    }

    public class CreateNewLectureCommandHandler : IRequestHandler<CreateNewLectureCommand, LessonLectureViewModel>
    {
        private readonly ILessonTopicQueryRepository _lessonTopicQueryRepository;
        private readonly ILessonTopicCommandRepository _lessonTopicCommandRepository;
        private readonly ILogger<CreateNewLectureCommandHandler> _logger;

        public CreateNewLectureCommandHandler(
            ILessonTopicQueryRepository lessonTopicQueryRepository,
            ILessonTopicCommandRepository lessonTopicCommandRepository,
            ILogger<CreateNewLectureCommandHandler> logger)
        {
            this._lessonTopicQueryRepository = lessonTopicQueryRepository;
            this._lessonTopicCommandRepository = lessonTopicCommandRepository;
            this._logger = logger;
        }

        public async Task<LessonLectureViewModel> Handle(CreateNewLectureCommand request, CancellationToken cancellationToken)
        {
            var response = new LessonLectureViewModel();

            try
            {
                var topic = await _lessonTopicQueryRepository.GetById(request.topicId, cancellationToken);

                var lectureName = string.Format("Lecture {0}", topic.LessonLectures.Count + 1);

                var lecture = new LessonLecture()
                {
                    Name = lectureName,
                };

                topic.LessonLectures.Add(lecture);

                await _lessonTopicCommandRepository.UpdateAsync(topic, cancellationToken);

                response = lecture.ToVM();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return response;
        }
    }
}
