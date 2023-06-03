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

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonPrerequisite
{
    public class SaveLessonPrerequisiteCommand : IRequest<ResponseViewModel>
    {
        public LessonPrerequisiteForm Vm { get; set; }
    }

    public class SaveLessonPrerequisiteCommandHandler : IRequestHandler<SaveLessonPrerequisiteCommand, ResponseViewModel>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;
        private readonly ILessonDesignService _lessonDesignService;
        private readonly ILogger<SaveLessonPrerequisiteCommand> _logger;

        public SaveLessonPrerequisiteCommandHandler(
            ILessonQueryRepository lessonQueryRepository, 
            ILessonCommandRepository lessonCommandRepository,
            ILessonDesignService lessonDesignService,
            ILogger<SaveLessonPrerequisiteCommand> logger)
        {
            this._lessonQueryRepository = lessonQueryRepository;
            this._lessonCommandRepository = lessonCommandRepository;
            this._lessonDesignService = lessonDesignService;
            this._logger = logger;

        }

        public async Task<ResponseViewModel> Handle(SaveLessonPrerequisiteCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var lesson = await _lessonQueryRepository
                    .GetById(request.Vm.LessonId, cancellationToken);

                _lessonDesignService.HandleLessonPrerequisites(lesson, request.Vm);

                await _lessonCommandRepository.UpdateAsync(lesson, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Lesson Prerequisites saved successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the lesson Prerequisites. Please try again.";
            }

            return response;
        }
    }
}
