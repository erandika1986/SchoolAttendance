using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public  class GradeSubject
    {
        public int SubjectId { get; set; }
        public int GradeId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
