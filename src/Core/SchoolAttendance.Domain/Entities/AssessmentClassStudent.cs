using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class AssessmentClassStudent
    {
        public int AssessmentId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public decimal? Score { get; set; }
        public decimal? ScorePrecentaged { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string ConnectedIp { get; set; }
        public string ConnectedBrowser { get; set; }

        public virtual Assessment Assessment { get; set; }
        public virtual Class Class { get; set; }
        public virtual User Student { get; set; }
    }
}
