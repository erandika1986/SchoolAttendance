﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class Assessment
    {
        public Assessment()
        {
            AssessmentClassStudents = new HashSet<AssessmentClassStudent>();
            AssessmentClasses = new HashSet<AssessmentClass>();
            AssessmentSections = new HashSet<AssessmentSection>();
            StudentAssessmentScores = new HashSet<StudentAssessmentScore>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? AcademicYearId { get; set; }
        public int? GradeId { get; set; }
        public int? SubjectId { get; set; }
        public int? AssessmentTypeId { get; set; }
        public int Statue { get; set; }
        public int VersionNo { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? PublishedOn { get; set; }
        public DateTime CompletedOn { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual User ApprovedByNavigation { get; set; }
        public virtual AssessmentType AssessmentType { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual ICollection<AssessmentClassStudent> AssessmentClassStudents { get; set; }
        public virtual ICollection<AssessmentClass> AssessmentClasses { get; set; }
        public virtual ICollection<AssessmentSection> AssessmentSections { get; set; }
        public virtual ICollection<StudentAssessmentScore> StudentAssessmentScores { get; set; }
    }
}
