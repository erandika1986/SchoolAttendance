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
    public class LessonAssignedClassConfiguration : IEntityTypeConfiguration<LessonAssignedClass>
    {
        public void Configure(EntityTypeBuilder<LessonAssignedClass> builder)
        {
            builder.HasKey(e => new { e.ClassId, e.LessonId });

            builder.ToTable("LessonAssignedClass");

            builder.Property(e => e.CompletedDate).HasColumnType("datetime");

            builder.Property(e => e.PublishedDate).HasColumnType("datetime");

            builder.Property(e => e.StartedDate).HasColumnType("datetime");

            builder.HasOne(d => d.Class)
                .WithMany(p => p.LessonAssignedClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonAssignedClass_Class");

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.LessonAssignedClasses)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonAssignedClass_Lesson");
        }
    }
}
