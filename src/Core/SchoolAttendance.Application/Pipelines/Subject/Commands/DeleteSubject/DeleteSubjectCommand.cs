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

namespace SchoolAttendance.Application.Pipelines.Subject.Commands.DeleteSubject
{
    public record DeleteSubjectCommand(int id) : IRequest<ResponseViewModel>
    {
    }

    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, ResponseViewModel>
    {
        private readonly ISubjectQueryRepository _subjectQueryRepository;
        private readonly ISubjectCommandRepository _subjectCommandRepository;
        private readonly ILogger<DeleteSubjectCommandHandler> _logger;  

        public DeleteSubjectCommandHandler(
            ISubjectQueryRepository subjectQueryRepository, 
            ISubjectCommandRepository subjectCommandRepository,
            ILogger<DeleteSubjectCommandHandler> logger
            )
        {
            this._subjectQueryRepository = subjectQueryRepository;
            this._subjectCommandRepository = subjectCommandRepository;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var subject = await _subjectQueryRepository.GetById(request.id, cancellationToken);

                subject.IsActive = false;

                foreach (var item in subject.ClassSubjects)
                {
                    item.IsActive = false;
                }

                foreach (var item in subject.GradeSubjects)
                {
                    item.IsActive = false;
                }

                await _subjectCommandRepository.UpdateAsync(subject, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Subject has been deactivated from the system.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while deactivating from the system.";
            }


            return response;
        }
    }

}
