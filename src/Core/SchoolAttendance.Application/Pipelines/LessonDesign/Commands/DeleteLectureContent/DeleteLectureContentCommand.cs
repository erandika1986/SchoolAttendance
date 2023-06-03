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

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.DeleteLectureContent
{
    public record DeleteLectureContentCommand(int id) : IRequest<ResponseViewModel>
    {
    }

    public class DeleteLectureContentCommandHandler : IRequestHandler<DeleteLectureContentCommand, ResponseViewModel>
    {
        private readonly ILessonLectureQueryRepository _lessonLectureQueryRepository;
        private readonly ILessonLectureCommandRepository _lessonLectureCommandRepository;
        private readonly ILogger<DeleteLectureContentCommandHandler> _logger;

        public DeleteLectureContentCommandHandler(
            ILessonLectureQueryRepository lessonLectureQueryRepository, 
            ILessonLectureCommandRepository lessonLectureCommandRepository, 
            ILogger<DeleteLectureContentCommandHandler> logger)
        {
            this._lessonLectureQueryRepository = lessonLectureQueryRepository;
            this._lessonLectureCommandRepository = lessonLectureCommandRepository;
            this._logger = logger;
        }


        public async Task<ResponseViewModel> Handle(DeleteLectureContentCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var lecture = await _lessonLectureQueryRepository.GetById(request.id, cancellationToken);
                lecture.LectureContentTypeId = (int?)null;
                lecture.LectureContent = "";
                lecture.Mimetype = "";

                await _lessonLectureCommandRepository.UpdateAsync(lecture, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Lecture content has been deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while clearing lecture content.";
            }

            return response;
        }
    }
}
