using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.CopyLesson
{
    public record CopyLessonCommand(int lessonId) : IRequest<ResponseViewModel>
    {
    }

    public class CopyLessonCommandHandler : IRequestHandler<CopyLessonCommand, ResponseViewModel>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;

        public CopyLessonCommandHandler(
            ICurrentUserService currentUserService, 
            ILessonQueryRepository lessonQueryRepository, 
            ILessonCommandRepository lessonCommandRepository)
        {
            this._currentUserService = currentUserService;
            this._lessonQueryRepository = lessonQueryRepository;
            this._lessonCommandRepository = lessonCommandRepository;
        }

        public Task<ResponseViewModel> Handle(CopyLessonCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
