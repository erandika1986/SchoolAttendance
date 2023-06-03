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

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.DeleteLecture
{
    public record DeleteLectureCommand(int  id) : IRequest<ResponseViewModel>
    {
    }

    public class DeleteLectureCommandHandler : IRequestHandler<DeleteLectureCommand, ResponseViewModel>
    {
        private readonly ILessonLectureQueryRepository _lessonLectureQueryRepository;
        private readonly ILessonLectureCommandRepository _lessonLectureCommandRepository;
        private readonly ILogger<DeleteLectureCommandHandler> _logger;

        public DeleteLectureCommandHandler(
            ILessonLectureQueryRepository lessonLectureQueryRepository, 
            ILessonLectureCommandRepository lessonLectureCommandRepository, 
            ILogger<DeleteLectureCommandHandler> logger)
        {
            this._lessonLectureQueryRepository = lessonLectureQueryRepository;
            this._lessonLectureCommandRepository = lessonLectureCommandRepository;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(DeleteLectureCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var lecture = await _lessonLectureQueryRepository.GetById(request.id, cancellationToken);

                await _lessonLectureCommandRepository.DeleteAsync(lecture, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Lecture has deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting the lecture.";
            }

            return response;
        }
    }
}
