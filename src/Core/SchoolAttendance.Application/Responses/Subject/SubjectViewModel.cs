using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
    public class SubjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Medium { get; set; }
        public string DepartmentHeadName { get; set; }
        public int DepartmentHeadId { get; set; }
        public bool IsParentSubject { get; set; }
        public bool IsBasketSubject { get; set; }
        public string ParentSubjectName { get; set; }
        public int? ParentSubjectId { get; set; }
        public string AssignedGrades { get; set; }
    }
}
