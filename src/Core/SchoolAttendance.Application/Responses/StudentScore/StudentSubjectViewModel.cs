using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses.StudentScore
{
    public  class StudentSubjectViewModel
    {
        public int Id { get; set; }
        public long AcademicYearId { get; set; }
        public long StudentId { get; set; }
        public long AcademicLevelId { get; set; }
        public long SubjectId { get; set; }

        public bool IsActive { get; set; }
    }
}
