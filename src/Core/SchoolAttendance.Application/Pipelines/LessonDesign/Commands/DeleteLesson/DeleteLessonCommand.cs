using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.DeleteLesson
{
    public record DeleteLessonCommand(int id) : IRequest<ResponseViewModel>
    {
    }

    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, ResponseViewModel>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;
        private readonly ILogger<DeleteLessonCommandHandler> _logger;

        public DeleteLessonCommandHandler(
            ILessonQueryRepository lessonQueryRepository, 
            ILessonCommandRepository lessonCommandRepository, 
            ILogger<DeleteLessonCommandHandler> logger)
        {
            this._lessonQueryRepository = lessonQueryRepository;
            this._lessonCommandRepository = lessonCommandRepository;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {

                var lesson = await _lessonQueryRepository.GetById(request.id, cancellationToken);

                lesson.IsActive = false;

                await _lessonCommandRepository.UpdateAsync(lesson, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Lesson has been deleted successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                response.IsSuccess = false;
                response.Message = "An error has been occured while deleting the lesson. Please try again.";
            }

            return response;
        }
    }
}
