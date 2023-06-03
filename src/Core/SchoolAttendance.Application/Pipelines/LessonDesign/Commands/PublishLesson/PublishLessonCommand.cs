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

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.PublishLesson
{
    public record PublishLessonCommand(int id): IRequest<ResponseViewModel>
    {
    }

    public class PublishLessonCommandHandler : IRequestHandler<PublishLessonCommand, ResponseViewModel>
    {
        private readonly ILessonQueryRepository _lessonQueryRepository;
        private readonly ILessonCommandRepository _lessonCommandRepository;
        private readonly ILogger<PublishLessonCommandHandler> _logger;   

        public PublishLessonCommandHandler(
            ILessonQueryRepository lessonQueryRepository, 
            ILessonCommandRepository lessonCommandRepository, 
            ILogger<PublishLessonCommandHandler> logger)
        {
            this._lessonQueryRepository = lessonQueryRepository;
            this._lessonCommandRepository = lessonCommandRepository;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(PublishLessonCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
