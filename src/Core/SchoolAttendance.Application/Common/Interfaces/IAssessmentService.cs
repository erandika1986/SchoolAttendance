using SchoolAttendance.Application.Responses;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Common.Interfaces
{
    public interface IAssessmentService
    {
        Task<AssessmentViewModel> SaveAssessment(AssessmentViewModel assessmentVm);
        Task<ResponseViewModel> DeleteAssessment(int id);
        Task<ResponseViewModel> Publish(AssessmentViewModel assessmentVm);
        Task<ResponseViewModel> MarkAsCompleted(AssessmentViewModel assessmentVm);
    }
}