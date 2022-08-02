using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class Grade
    {
        public Grade()
        {
            Assessments = new HashSet<Assessment>();
            Classes = new HashSet<Class>();
            GradeSubjects = new HashSet<GradeSubject>();
            Lessons = new HashSet<Lesson>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? LevelHeadId { get; set; }
        public bool IsActive { get; set; }

        public virtual User LevelHead { get; set; }
        public virtual ICollection<Assessment> Assessments { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<GradeSubject> GradeSubjects { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
