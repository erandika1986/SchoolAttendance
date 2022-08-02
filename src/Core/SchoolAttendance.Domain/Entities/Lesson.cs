using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolAttendance.Domain.Entities
{
    public  class Lesson
    {
        public Lesson()
        {
            LessonAssignedClasses = new HashSet<LessonAssignedClass>();
            LessonAssignments = new HashSet<LessonAssignment>();
            LessonLearningOutcomes = new HashSet<LessonLearningOutcome>();
            LessonPrerequisites = new HashSet<LessonPrerequisite>();
            LessonTopics = new HashSet<LessonTopic>();
            LessonUnitTests = new HashSet<LessonUnitTest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LessonIntroduction { get; set; }
        public decimal? Duration { get; set; }
        public string CompetencyLevel { get; set; }
        public string TeachingAids { get; set; }
        public int LessonOwnerId { get; set; }
        public int? AcademicYearId { get; set; }
        public int? GradeId { get; set; }
        public int? SubjectId { get; set; }
        public int Status { get; set; }
        public string TeachingProcess { get; set; }
        public bool HasLessonTest { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual User LessonOwner { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual ICollection<LessonAssignedClass> LessonAssignedClasses { get; set; }
        public virtual ICollection<LessonAssignment> LessonAssignments { get; set; }
        public virtual ICollection<LessonLearningOutcome> LessonLearningOutcomes { get; set; }
        public virtual ICollection<LessonPrerequisite> LessonPrerequisites { get; set; }
        public virtual ICollection<LessonTopic> LessonTopics { get; set; }
        public virtual ICollection<LessonUnitTest> LessonUnitTests { get; set; }
    }
}
