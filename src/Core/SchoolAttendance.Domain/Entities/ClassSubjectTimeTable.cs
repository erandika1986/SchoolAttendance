using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class ClassSubjectTimeTable : BaseEntity
    {
        public ClassSubjectTimeTable()
        {
            SubjectAttendances = new HashSet<SubjectAttendance>();
        }

        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int DayId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Class Class { get; set; }
        public virtual Day Day { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<SubjectAttendance> SubjectAttendances { get; set; }
    }
}
