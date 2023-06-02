using SchoolAttendance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
    public class AssessmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AcademicYearId { get; set; }
        public int GradeId { get; set; }
        public int SubjectId { get; set; }
        public int AssessmentTypeId { get; set; }
        public string AssignedClassNames { get; set; }
        public List<int> AssignedClasses { get; set; }
        public AssessmentConductBy? AssessmentConductBy { get; set; }
        public AssessmentStatus Status { get; set; }
        public int? VersionNo { get; set; }
        public string ApprovedBy { get; set; }
        public bool IsActive { get; set; }
        public string PublishedOn { get; set; }
        public string CompletedOn { get; set; }
    }
}
