using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.CreateNewLecture;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLectureName
{
    public class SaveLessonLectureNameCommand : IRequest<ResponseViewModel>
    {
        public LessonLectureViewModel Vm { get; set; }
    }

    public class SaveLessonLectureNameCommandHandler : IRequestHandler<SaveLessonLectureNameCommand, ResponseViewModel>
    {
        private readonly ILessonLectureQueryRepository _lessonLectureQueryRepository;
        private readonly ILessonLectureCommandRepository _lessonLectureCommandRepository;
        private readonly ILogger<SaveLessonLectureNameCommandHandler> _logger;

        public SaveLessonLectureNameCommandHandler(
            ILessonLectureQueryRepository lessonLectureQueryRepository, 
            ILessonLectureCommandRepository lessonLectureCommandRepository,
            ILogger<SaveLessonLectureNameCommandHandler> logger)
        {
            this._lessonLectureQueryRepository = lessonLectureQueryRepository;
            this._lessonLectureCommandRepository = lessonLectureCommandRepository;
            this._logger = logger;
        }


        public async Task<ResponseViewModel> Handle(SaveLessonLectureNameCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();
            try
            {
                var lecture = await _lessonLectureQueryRepository.GetById(request.Vm.Id, cancellationToken);

                lecture.Name = request.Vm.Name;

                await _lessonLectureCommandRepository.UpdateAsync(lecture, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Lecture name successfully saved.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the lecture name";
            }

            return response;
        }
    }
}
