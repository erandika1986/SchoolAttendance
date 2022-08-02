using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public  class Class
    {
        public Class()
        {
            AssessmentClassStudents = new HashSet<AssessmentClassStudent>();
            AssessmentClasses = new HashSet<AssessmentClass>();
            ClassSubjectTimeTables = new HashSet<ClassSubjectTimeTable>();
            ClassSubjects = new HashSet<ClassSubject>();
            LessonAssignedClasses = new HashSet<LessonAssignedClass>();
            StudentClasses = new HashSet<StudentClass>();
            SubjectAttendances = new HashSet<SubjectAttendance>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassTeacherId { get; set; }
        public int GradeId { get; set; }
        public int AcademicYear { get; set; }

        public virtual AcademicYear AcademicYearNavigation { get; set; }
        public virtual User ClassTeacher { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual ICollection<AssessmentClassStudent> AssessmentClassStudents { get; set; }
        public virtual ICollection<AssessmentClass> AssessmentClasses { get; set; }
        public virtual ICollection<ClassSubjectTimeTable> ClassSubjectTimeTables { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        public virtual ICollection<LessonAssignedClass> LessonAssignedClasses { get; set; }
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
        public virtual ICollection<SubjectAttendance> SubjectAttendances { get; set; }
    }
}
