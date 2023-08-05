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

namespace SchoolAttendance.Application.Pipelines.Assessment.Commands.SaveAssessment
{
    public class SaveAssessmentCommand : IRequest<AssessmentViewModel>
    {
        public AssessmentViewModel AssessmentVm { get; set; }
    }

    public class SaveAssessmentCommandHandler : IRequestHandler<SaveAssessmentCommand, AssessmentViewModel>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAssessmentQueryRepository _assessmentQueryRepository;
        private readonly IAssessmentCommandRepository _assessmentCommandRepository;

        public SaveAssessmentCommandHandler(
            ICurrentUserService currentUserService, 
            IAssessmentQueryRepository assessmentQueryRepository, 
            IAssessmentCommandRepository assessmentCommandRepository)
        {
            this._currentUserService = currentUserService;
            this._assessmentQueryRepository = assessmentQueryRepository;
            this._assessmentCommandRepository = assessmentCommandRepository;
        }

        public async Task<AssessmentViewModel> Handle(SaveAssessmentCommand request, CancellationToken cancellationToken)
        {
           

         
        }
    }
}
