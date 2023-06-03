using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.Attendance.Queries.GetAttendanceDetailForSubjectClassById;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.SaveLessonDetail
{
    public class SaveLessonDetailCommand : IRequest<ResponseViewModel>
    {
        public LessonDetailViewModel Vm { get; set; }
    }

    public class SaveLessonDetailCommandHandler : IRequestHandler<SaveLessonDetailCommand, ResponseViewModel>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<SaveLessonDetailCommandHandler> _logger;
        private readonly ILessonDesignService _lessonDesignService;

        public SaveLessonDetailCommandHandler(
            ILessonQueryRepository lessonQueryRepository, 
            ILessonCommandRepository lessonCommandRepository, 
            ICurrentUserService currentUserService,
            ILogger<SaveLessonDetailCommandHandler> logger,
            ILessonDesignService lessonDesignService)
        {
            this._lessonQueryRepository = lessonQueryRepository;
            this._lessonCommandRepository = lessonCommandRepository;
            this._currentUserService = currentUserService;
            this._logger = logger;
            this._lessonDesignService = lessonDesignService;
        }

        public async Task<ResponseViewModel> Handle(SaveLessonDetailCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {

                var lesson = await _lessonQueryRepository.GetById(request.Vm.LessonId, cancellationToken);

                lesson.Name = request.Vm.Name;
                lesson.LessonIntroduction = request.Vm.LessonIntroduction;
                lesson.Duration = request.Vm.Duration;
                lesson.CompetencyLevel = request.Vm.CompetencyLevel;
                lesson.TeachingAids = string.Join(",", request.Vm.TeacherAids);
                lesson.LessonOwnerId = _currentUserService.UserId.Value;
                lesson.GradeId = request.Vm.GradeId;
                lesson.SubjectId = request.Vm.SubjectId;
                lesson.TeachingProcess = request.Vm.TeachingProcess;
                lesson.IsActive = true;
                lesson.HasLessonTest = request.Vm.HasLessonTest;

                //Handle Lesson Classes
                _lessonDesignService.HandleLessonClasses(lesson, request.Vm);

                await _lessonCommandRepository.UpdateAsync(lesson, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Lesson detail saved successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving the lesson details. Please try again.";
            }

            return response;
        }
    }
}
