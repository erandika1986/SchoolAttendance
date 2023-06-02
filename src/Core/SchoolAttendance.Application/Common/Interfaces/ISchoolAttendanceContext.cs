using Microsoft.EntityFrameworkCore;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Common.Interfaces
{
    public interface ISchoolAttendanceContext
    {
        DbSet<AcademicYear> AcademicYears { get; }
        DbSet<Assessment> Assessments { get; }

        DbSet<AssessmentUpload> AssessmentsUploads { get; }
        DbSet<AssessmentClass> AssessmentClasses { get; }
        DbSet<AssessmentClassStudent> AssessmentClassStudents { get; }
        DbSet<AssessmentMcqquestionStudentAnswer> AssessmentMcqquestionStudentAnswers { get; }
        DbSet<AssessmentOpenEndedQuestionStudentAnswer> AssessmentOpenEndedQuestionStudentAnswers { get; }
        DbSet<AssessmentSection> AssessmentSections { get; }
        DbSet<AssessmentSectionQuestion> AssessmentSectionQuestions { get; }
        DbSet<AssessmentSectionStudentQuestion> AssessmentSectionStudentQuestions { get; }
        DbSet<AssessmentStructuredQuestionStudentAnswer> AssessmentStructuredQuestionStudentAnswers { get; }
        DbSet<AssessmentType> AssessmentTypes { get; }
        DbSet<Class> Classes { get; }
        DbSet<ClassSubject> ClassSubjects { get; }
        DbSet<ClassSubjectStudent> ClassSubjectStudents { get; }
        DbSet<ClassSubjectTimeTable> ClassSubjectTimeTables { get; }
        DbSet<Day> Days { get; }
        DbSet<Grade> Grades { get; }
        DbSet<GradeSubject> GradeSubjects { get; }
        DbSet<Lesson> Lessons { get; }
        DbSet<LessonAssignedClass> LessonAssignedClasses { get; }
        DbSet<LessonAssignment> LessonAssignments { get; }
        DbSet<LessonAssignmentStudent> LessonAssignmentStudents { get; }
        DbSet<LessonAssignmentStudentUpload> LessonAssignmentStudentUploads { get; }
        DbSet<LessonLearningOutcome> LessonLearningOutcomes { get; }
        DbSet<LessonLecture> LessonLectures { get; }
        DbSet<LessonLectureContentType> LessonLectureContentTypes { get; }
        DbSet<LessonPrerequisite> LessonPrerequisites { get; }
        DbSet<LessonTopic> LessonTopics { get; }
        DbSet<LessonUnitTest> LessonUnitTests { get; }
        DbSet<LessonUnitTestTopic> LessonUnitTestTopics { get; }
        DbSet<LessonUnitTestTopicQuestion> LessonUnitTestTopicQuestions { get; }
        DbSet<LessonUnitTestTopicStudentMcqquestionAnswer> LessonUnitTestTopicStudentMcqquestionAnswers { get; }
        DbSet<LessonUnitTestTopicStudentOpenEndedQuestionAnswer> LessonUnitTestTopicStudentOpenEndedQuestionAnswers { get; }
        DbSet<LessonUnitTestTopicStudentQuestion> LessonUnitTestTopicStudentQuestions { get; }
        DbSet<Question> Questions { get; }
        DbSet<QuestionMcqteacherAnswer> QuestionMcqteacherAnswers { get; }
        DbSet<QuestionOpenEndedTeacherAnswer> QuestionOpenEndedTeacherAnswers { get; }
        DbSet<QuestionStructured> QuestionStructureds { get; }
        DbSet<QuestionStructuredTeacherAnswer> QuestionTructuredTeacherAnswers { get; }
        DbSet<QuestionType> QuestionTypes { get; }
        DbSet<Role> Roles { get; }
        DbSet<StudentAssessmentScore> StudentAssessmentScores { get; }
        DbSet<StudentClass> StudentClasses { get; }
        DbSet<StudentSubjectAttendance> StudentSubjectAttendances { get; }
        DbSet<Subject> Subjects { get; }
        DbSet<SubjectAttendance> SubjectAttendances { get; }
        DbSet<SubjectTeacher> SubjectTeachers { get; }
        DbSet<User> Users { get; }
        DbSet<UserRole> UserRoles { get; }

        Task<int> SaveChangesAsync();
    }
}
