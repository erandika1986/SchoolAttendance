using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class ClassSubjectStudent
    {
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? DeAllocatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
