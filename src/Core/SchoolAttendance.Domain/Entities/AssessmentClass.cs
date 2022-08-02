using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class AssessmentClass
    {
        public int ClassId { get; set; }
        public int AssessmentId { get; set; }
        public DateTime PublishedOn { get; set; }

        public virtual Assessment Assessment { get; set; }
        public virtual Class Class { get; set; }
    }
}
