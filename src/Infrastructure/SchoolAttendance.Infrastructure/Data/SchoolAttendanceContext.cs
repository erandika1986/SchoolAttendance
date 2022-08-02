using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Domain.Entities;
using SchoolAttendance.Infrastructure.Interceptors;
using System.Reflection;

namespace SchoolAttendance.Infrastructure.Data
{
    public  class SchoolAttendanceContext : DbContext, ISchoolAttendanceContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;


        public SchoolAttendanceContext(DbContextOptions<SchoolAttendanceContext> options, IMediator mediator, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
        {
            this._mediator = mediator;
            this._auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public async Task<int> SaveChangesAsync()
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync();
        }

        public virtual DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();
        public virtual DbSet<Assessment> Assessments => Set<Assessment>();
        public virtual DbSet<AssessmentClass> AssessmentClasses => Set<AssessmentClass>();
        public virtual DbSet<AssessmentClassStudent> AssessmentClassStudents => Set<AssessmentClassStudent>();
        public virtual DbSet<AssessmentMcqquestionStudentAnswer> AssessmentMcqquestionStudentAnswers => Set<AssessmentMcqquestionStudentAnswer>();
        public virtual DbSet<AssessmentOpenEndedQuestionStudentAnswer> AssessmentOpenEndedQuestionStudentAnswers => Set<AssessmentOpenEndedQuestionStudentAnswer>();
        public virtual DbSet<AssessmentSection> AssessmentSections => Set<AssessmentSection>();
        public virtual DbSet<AssessmentSectionQuestion> AssessmentSectionQuestions => Set<AssessmentSectionQuestion>();
        public virtual DbSet<AssessmentSectionStudentQuestion> AssessmentSectionStudentQuestions => Set<AssessmentSectionStudentQuestion>();
        public virtual DbSet<AssessmentStructuredQuestionStudentAnswer> AssessmentStructuredQuestionStudentAnswers => Set<AssessmentStructuredQuestionStudentAnswer>();
        public virtual DbSet<AssessmentType> AssessmentTypes => Set<AssessmentType>();
        public virtual DbSet<Class> Classes => Set<Class>();
        public virtual DbSet<ClassSubject> ClassSubjects => Set<ClassSubject>();
        public virtual DbSet<ClassSubjectStudent> ClassSubjectStudents => Set<ClassSubjectStudent>();
        public virtual DbSet<ClassSubjectTimeTable> ClassSubjectTimeTables => Set<ClassSubjectTimeTable>();
        public virtual DbSet<Day> Days => Set<Day>();
        public virtual DbSet<Grade> Grades => Set<Grade>();
        public virtual DbSet<GradeSubject> GradeSubjects => Set<GradeSubject>();
        public virtual DbSet<Lesson> Lessons => Set<Lesson>();
        public virtual DbSet<LessonAssignedClass> LessonAssignedClasses => Set<LessonAssignedClass>();
        public virtual DbSet<LessonAssignment> LessonAssignments => Set<LessonAssignment>();
        public virtual DbSet<LessonAssignmentStudent> LessonAssignmentStudents => Set<LessonAssignmentStudent>();
        public virtual DbSet<LessonAssignmentStudentUpload> LessonAssignmentStudentUploads => Set<LessonAssignmentStudentUpload>();
        public virtual DbSet<LessonLearningOutcome> LessonLearningOutcomes => Set<LessonLearningOutcome>();
        public virtual DbSet<LessonLecture> LessonLectures => Set<LessonLecture>();
        public virtual DbSet<LessonLectureContentType> LessonLectureContentTypes => Set<LessonLectureContentType>();
        public virtual DbSet<LessonPrerequisite> LessonPrerequisites => Set<LessonPrerequisite>();
        public virtual DbSet<LessonTopic> LessonTopics => Set<LessonTopic>();
        public virtual DbSet<LessonUnitTest> LessonUnitTests => Set<LessonUnitTest>();
        public virtual DbSet<LessonUnitTestTopic> LessonUnitTestTopics => Set<LessonUnitTestTopic>();
        public virtual DbSet<LessonUnitTestTopicQuestion> LessonUnitTestTopicQuestions => Set<LessonUnitTestTopicQuestion>();
        public virtual DbSet<LessonUnitTestTopicStudentMcqquestionAnswer> LessonUnitTestTopicStudentMcqquestionAnswers => Set<LessonUnitTestTopicStudentMcqquestionAnswer>();
        public virtual DbSet<LessonUnitTestTopicStudentOpenEndedQuestionAnswer> LessonUnitTestTopicStudentOpenEndedQuestionAnswers => Set<LessonUnitTestTopicStudentOpenEndedQuestionAnswer>();
        public virtual DbSet<LessonUnitTestTopicStudentQuestion> LessonUnitTestTopicStudentQuestions => Set<LessonUnitTestTopicStudentQuestion>();
        public virtual DbSet<Question> Questions => Set<Question>();
        public virtual DbSet<QuestionMcqteacherAnswer> QuestionMcqteacherAnswers => Set<QuestionMcqteacherAnswer>();
        public virtual DbSet<QuestionOpenEndedTeacherAnswer> QuestionOpenEndedTeacherAnswers => Set<QuestionOpenEndedTeacherAnswer>();
        public virtual DbSet<QuestionStructured> QuestionStructureds => Set<QuestionStructured>();
        public virtual DbSet<QuestionTructuredTeacherAnswer> QuestionTructuredTeacherAnswers => Set<QuestionTructuredTeacherAnswer>();
        public virtual DbSet<QuestionType> QuestionTypes => Set<QuestionType>();
        public virtual DbSet<Role> Roles => Set<Role>();
        public virtual DbSet<StudentAssessmentScore> StudentAssessmentScores => Set<StudentAssessmentScore>();
        public virtual DbSet<StudentClass> StudentClasses => Set<StudentClass>();
        public virtual DbSet<StudentSubjectAttendance> StudentSubjectAttendances => Set<StudentSubjectAttendance>();
        public virtual DbSet<Subject> Subjects => Set<Subject>();
        public virtual DbSet<SubjectAttendance> SubjectAttendances => Set<SubjectAttendance>();
        public virtual DbSet<SubjectTeacher> SubjectTeachers => Set<SubjectTeacher>();
        public virtual DbSet<User> Users => Set<User>();
        public virtual DbSet<UserRole> UserRoles => Set<UserRole>();
    }
}
