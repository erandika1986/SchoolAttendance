using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public User()
        {
            AssessmentApprovedByNavigations = new HashSet<Assessment>();
            AssessmentClassStudents = new HashSet<AssessmentClassStudent>();
            AssessmentCreatedBies = new HashSet<Assessment>();
            AssessmentSectionStudentQuestions = new HashSet<AssessmentSectionStudentQuestion>();
            AssessmentUpdatedBies = new HashSet<Assessment>();
            ClassSubjects = new HashSet<ClassSubject>();
            Classes = new HashSet<Class>();
            Grades = new HashSet<Grade>();
            LessonAssignmentStudents = new HashSet<LessonAssignmentStudent>();
            LessonCreatedBies = new HashSet<Lesson>();
            LessonLessonOwners = new HashSet<Lesson>();
            LessonUnitTestTopicStudentQuestions = new HashSet<LessonUnitTestTopicStudentQuestion>();
            LessonUpdatedBies = new HashSet<Lesson>();
            Questions = new HashSet<Question>();
            StudentAssessmentScores = new HashSet<StudentAssessmentScore>();
            StudentClasses = new HashSet<StudentClass>();
            StudentSubjectAttendances = new HashSet<StudentSubjectAttendance>();
            SubjectTeachers = new HashSet<SubjectTeacher>();
            Subjects = new HashSet<Subject>();
            UserRoles = new HashSet<UserRole>();
        }


        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string TimeZoneId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? EmailConfirmationCode { get; set; }
        public DateTime? LastLoggedOn { get; set; }

        public virtual ICollection<Assessment> AssessmentApprovedByNavigations { get; set; }
        public virtual ICollection<AssessmentClassStudent> AssessmentClassStudents { get; set; }
        public virtual ICollection<Assessment> AssessmentCreatedBies { get; set; }
        public virtual ICollection<AssessmentSectionStudentQuestion> AssessmentSectionStudentQuestions { get; set; }
        public virtual ICollection<Assessment> AssessmentUpdatedBies { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<LessonAssignmentStudent> LessonAssignmentStudents { get; set; }
        public virtual ICollection<Lesson> LessonCreatedBies { get; set; }
        public virtual ICollection<Lesson> LessonLessonOwners { get; set; }
        public virtual ICollection<LessonUnitTestTopicStudentQuestion> LessonUnitTestTopicStudentQuestions { get; set; }
        public virtual ICollection<Lesson> LessonUpdatedBies { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<StudentAssessmentScore> StudentAssessmentScores { get; set; }
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
        public virtual ICollection<StudentSubjectAttendance> StudentSubjectAttendances { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
