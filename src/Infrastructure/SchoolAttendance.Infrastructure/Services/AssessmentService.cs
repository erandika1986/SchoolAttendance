using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;

namespace SchoolAttendance.Infrastructure.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly ISchoolAttendanceContext db;
        private readonly ILogger<IAttendanceService> logger;

        public AssessmentService(ISchoolAttendanceContext db, ILogger<IAttendanceService> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task<AssessmentViewModel> SaveAssessment(AssessmentViewModel assessmentVm)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseViewModel> DeleteAssessment(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseViewModel> Publish(AssessmentViewModel assessmentVm)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseViewModel> MarkAsCompleted(AssessmentViewModel assessmentVm)
        {
            throw new System.NotImplementedException();
        }
    }
}