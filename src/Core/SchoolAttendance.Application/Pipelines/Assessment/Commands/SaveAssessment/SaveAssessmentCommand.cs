using MediatR;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
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
        private readonly IAssessmentService _assessmentService;

        public SaveAssessmentCommandHandler(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        public async Task<AssessmentViewModel> Handle(SaveAssessmentCommand request, CancellationToken cancellationToken)
        {
            var response = await _assessmentService.SaveAssessment(request.AssessmentVm);

            return response;
        }
    }
}
