using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;

namespace SchoolAttendance.Infrastructure.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly ISchoolAttendanceContext db;
        private readonly ILogger<IAttendanceService> logger;
        private readonly ICoreDataService coreDataService;

        public AssessmentService(ISchoolAttendanceContext db, ILogger<IAttendanceService> logger, ICoreDataService coreDataService)
        {
            this.db = db;
            this.logger = logger;
            this.coreDataService = coreDataService;
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

        public Task<ResponseViewModel> CreateMidAssessmentForSelectedGrades()
        {
            throw new NotImplementedException();
        }
    }
}