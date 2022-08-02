using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class StudentAssessmentScore
    {
        public int StudentId { get; set; }
        public int AssessmentId { get; set; }
        public decimal? PredictedTargetScore { get; set; }
        public DateTime TargetGeneratedOn { get; set; }
        public decimal? TeacherAdjustedTargetScore { get; set; }
        public DateTime? TargetAdjustedOn { get; set; }
        public decimal? ActualScore { get; set; }
        public DateTime? ActualScoreEnteredOn { get; set; }

        public virtual Assessment Assessment { get; set; }
        public virtual User Student { get; set; }
    }
}
