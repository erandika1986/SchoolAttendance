using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAttendance.Core.Entities;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Data.Configurations
{
    public class LessonLearningOutcomeConfiguration : IEntityTypeConfiguration<LessonLearningOutcome>
    {
        public void Configure(EntityTypeBuilder<LessonLearningOutcome> builder)
        {
            builder.ToTable("LessonLearningOutcome");

            builder.Property(e => e.LearningOutcome)
                .IsRequired()
                .HasMaxLength(4000);

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.LessonLearningOutcomes)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonLearningOutcome_Lesson");
        }
    }
}
