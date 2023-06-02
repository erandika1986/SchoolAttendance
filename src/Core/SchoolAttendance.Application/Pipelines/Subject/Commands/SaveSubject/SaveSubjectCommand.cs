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

namespace SchoolAttendance.Application.Pipelines.Subject.Commands.SaveSubject
{
    public record SaveSubjectCommand(SubjectViewModel vm) : IRequest<ResponseViewModel>
    {
    }

    public class SaveSubjectCommandHandler : IRequestHandler<SaveSubjectCommand, ResponseViewModel>
    {
        private readonly ISubjectQueryRepository _subjectQueryRepository;
        private readonly ISubjectCommandRepository _subjectCommandRepository;
        private readonly ILogger<SaveSubjectCommandHandler> _logger;

        public SaveSubjectCommandHandler(
            ISubjectQueryRepository subjectQueryRepository, 
            ISubjectCommandRepository subjectCommandRepository,
            ILogger<SaveSubjectCommandHandler> logger)
        {
            this._subjectQueryRepository = subjectQueryRepository;
            this._subjectCommandRepository = subjectCommandRepository;
            this._logger = logger;

        }

        public async Task<ResponseViewModel> Handle(SaveSubjectCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var existingSubject = 
                    (
                        await _subjectQueryRepository
                        .Query(x => x.Name.Trim().ToLower() == request.vm.Name.Trim().ToLower() && x.Id != request.vm.Id)
                     ).FirstOrDefault();

                if (existingSubject != null)
                {
                    response.IsSuccess = false;
                    response.Message = "Subject name already registered. Please enter different subject name.";

                    return response;
                }

                var subject = await _subjectQueryRepository.GetById(request.vm.Id, cancellationToken);

                if (subject == null)
                {
                    subject = new Domain.Entities.Subject()
                    {
                        Name = request.vm.Name,
                        Description = request.vm.Description,
                        Medium = request.vm.Medium,
                        IsActive = true
                    };

                    if (request.vm.DepartmentHeadId > 0)
                    {
                        subject.DepartmentHeadId = request.vm.DepartmentHeadId;
                    }

                    await _subjectCommandRepository.AddAsync(subject, cancellationToken);

                    response.Message = "New Subject has been saved successfully.";
                }
                else
                {
                    subject.Name = request.vm.Name;
                    subject.Description = request.vm.Description;
                    subject.Medium = request.vm.Medium;

                    if (request.vm.DepartmentHeadId > 0)
                    {
                        subject.DepartmentHeadId = request.vm.DepartmentHeadId;
                    }

                    await _subjectCommandRepository.UpdateAsync(subject, cancellationToken);

                    response.Message = "Subject has been updated successfully.";
                }

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while saving from the system.";
            }

            return response;
        }
    }
}
