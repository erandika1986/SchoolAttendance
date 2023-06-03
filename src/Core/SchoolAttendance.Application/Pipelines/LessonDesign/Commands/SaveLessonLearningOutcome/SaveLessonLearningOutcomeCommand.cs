using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonPrerequisite;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonLearningOutcome
{
    public class SaveLessonLearningOutcomeCommand : IRequest<ResponseViewModel>
    {
        public LessonOutcomeForm Vm { get; set; }
    }

    public class SaveLessonLearningOutcomeCommandHandler : IRequestHandler<SaveLessonLearningOutcomeCommand, ResponseViewModel>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;
        private readonly ILessonDesignService _lessonDesignService;
        private readonly ILogger<SaveLessonLearningOutcomeCommandHandler> _logger;

        public SaveLessonLearningOutcomeCommandHandler(
            ILessonQueryRepository lessonQueryRepository,
            ILessonCommandRepository lessonCommandRepository,
            ILessonDesignService lessonDesignService,
            ILogger<SaveLessonLearningOutcomeCommandHandler> logger)
        {
            this._lessonQueryRepository = lessonQueryRepository;
            this._lessonCommandRepository = lessonCommandRepository;
            this._lessonDesignService = lessonDesignService;
            this._logger = logger;

        }

        public async Task<ResponseViewModel> Handle(SaveLessonLearningOutcomeCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var lesson = await _lessonQueryRepository
                    .GetById(request.Vm.LessonId, cancellationToken);

                _lessonDesignService.HandleLessonLearningOutcome(lesson, request.Vm);

                await _lessonCommandRepository.UpdateAsync(lesson, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Lesson Learning Outcomes saved successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the lesson learning outcomes. Please try again.";
            }

            return response;
        }
    }
}
