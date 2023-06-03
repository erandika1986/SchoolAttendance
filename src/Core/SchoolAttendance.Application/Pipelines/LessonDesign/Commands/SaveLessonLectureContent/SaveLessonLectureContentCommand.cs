using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLectureName;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLectureContent
{
    public class SaveLessonLectureContentCommand : IRequest<LessonLectureViewModel>
    {
        public LessonLectureViewModel Vm { get; set; }
    }

    public class SaveLessonLectureContentCommandHandler : IRequestHandler<SaveLessonLectureContentCommand, LessonLectureViewModel>
    {
        private readonly ILessonLectureQueryRepository _lessonLectureQueryRepository;
        private readonly ILessonLectureCommandRepository _lessonLectureCommandRepository;
        private readonly ILogger<SaveLessonLectureContentCommandHandler> _logger;

        public SaveLessonLectureContentCommandHandler(
            ILessonLectureQueryRepository lessonLectureQueryRepository,
            ILessonLectureCommandRepository lessonLectureCommandRepository,
            ILogger<SaveLessonLectureContentCommandHandler> logger)
        {
            this._lessonLectureQueryRepository = lessonLectureQueryRepository;
            this._lessonLectureCommandRepository = lessonLectureCommandRepository;
            this._logger = logger;
        }
        public async Task<LessonLectureViewModel> Handle(SaveLessonLectureContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lecture = await _lessonLectureQueryRepository.GetById(request.Vm.Id, cancellationToken);

                lecture.LectureContentTypeId = request.Vm.ContentType;

                lecture.LectureContentTypeId = request.Vm.ContentType;

                if (request.Vm.ContentType == (int)LectureContentType.Youtube)
                {
                    lecture.LectureContent = request.Vm.Content.Replace("watch?v=", "embed/");
                }
                else
                {
                    lecture.LectureContent = request.Vm.Content;
                }

                await _lessonLectureCommandRepository.UpdateAsync(lecture, cancellationToken);

                request.Vm.Content = lecture.LectureContent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }


            return request.Vm;
        }
    }
}
