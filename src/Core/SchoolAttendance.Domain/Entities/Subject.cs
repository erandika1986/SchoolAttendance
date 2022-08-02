using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public class Subject
    {
        public Subject()
        {
            Assessments = new HashSet<Assessment>();
            ClassSubjectTimeTables = new HashSet<ClassSubjectTimeTable>();
            ClassSubjects = new HashSet<ClassSubject>();
            GradeSubjects = new HashSet<GradeSubject>();
            InverseParentSubject = new HashSet<Subject>();
            Lessons = new HashSet<Lesson>();
            Questions = new HashSet<Question>();
            SubjectAttendances = new HashSet<SubjectAttendance>();
            SubjectTeachers = new HashSet<SubjectTeacher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Medium { get; set; }
        public int? DepartmentHeadId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsBasketSubject { get; set; }
        public int? ParentSubjectId { get; set; }

        public virtual User DepartmentHead { get; set; }
        public virtual Subject ParentSubject { get; set; }
        public virtual ICollection<Assessment> Assessments { get; set; }
        public virtual ICollection<ClassSubjectTimeTable> ClassSubjectTimeTables { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        public virtual ICollection<GradeSubject> GradeSubjects { get; set; }
        public virtual ICollection<Subject> InverseParentSubject { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<SubjectAttendance> SubjectAttendances { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
    }
}
