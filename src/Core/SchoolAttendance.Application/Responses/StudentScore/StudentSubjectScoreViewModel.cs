using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses.StudentScore
{
    public class StudentSubjectScoreViewModel
    {
        public int AssessmentTypeId { get; set; }
        public long StudentSubjectId { get; set; }
        public decimal GainScore { get; set; }
        public decimal AllocatedScore { get; set; }
        public decimal ScoreDifference { get; set; }
        public int GradeId { get; set; }
        public int LevelRank { get; set; }
    }
}
