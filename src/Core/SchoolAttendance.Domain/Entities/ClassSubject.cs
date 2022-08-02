using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class ClassSubject
    {
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int? SubjectTeacherId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual User SubjectTeacher { get; set; }
    }
}
