using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.CreateNewLesson
{
    public class CreateNewLessonCommand : IRequest<LessonViewModel>
    {
    }

    public class CreateNewLessonCommandHandler : IRequestHandler<CreateNewLessonCommand, LessonViewModel>
    {
        private readonly ILessonCommandRepository _lessonCommandRepository;
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;
        private readonly ICurrentUserService _currentUserService;
        public CreateNewLessonCommandHandler(
            ILessonCommandRepository lessonCommandRepository, 
            ILessonQueryRepository lessonQueryRepository,
            IAcademicYearQueryRepository academicYearQueryRepository,
            ICurrentUserService currentUserService)
        {
            this._lessonCommandRepository = lessonCommandRepository;
            this._lessonQueryRepository = lessonQueryRepository;
            this._academicYearQueryRepository = academicYearQueryRepository;
            this._currentUserService = currentUserService;  
        }
        public async Task<LessonViewModel> Handle(CreateNewLessonCommand request, CancellationToken cancellationToken)
        {
            var currentAcademicYear = await _academicYearQueryRepository.GetCurrentAcademicYear(cancellationToken);
            var lessonCount = (await _lessonQueryRepository.Query(x => x.CreatedById == _currentUserService.UserId.Value)).Count();

            var lessonNo = string.Format("Lesson {0}", lessonCount + 1);

            var lesson = new Lesson()
            {
                Name = lessonNo,
                AcademicYearId = currentAcademicYear.Id,
                LessonOwnerId = _currentUserService.UserId.Value,
                Status = (int)LessonStatus.Design,
                IsActive = true
            };

            await _lessonCommandRepository.AddAsync(lesson, cancellationToken);

            var response = lesson.ToVM();

            return response;
        }
    }
}
