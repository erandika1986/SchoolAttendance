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
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lesson");

            builder.Property(e => e.CompetencyLevel).HasMaxLength(4000);

            builder.Property(e => e.CreatedOn).HasColumnType("datetime");

            builder.Property(e => e.Duration).HasColumnType("decimal(6, 2)");

            builder.Property(e => e.Name).HasMaxLength(1000);

            builder.Property(e => e.TeachingAids).HasMaxLength(100);

            builder.Property(e => e.UpdatedOn).HasColumnType("datetime");

            builder.HasOne(d => d.AcademicYear)
                .WithMany(p => p.Lessons)
                .HasForeignKey(d => d.AcademicYearId)
                .HasConstraintName("FK_Lesson_AcademicYear");

            builder.HasOne(d => d.CreatedBy)
                .WithMany(p => p.LessonCreatedBies)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lesson_User1");

            builder.HasOne(d => d.Grade)
                .WithMany(p => p.Lessons)
                .HasForeignKey(d => d.GradeId)
                .HasConstraintName("FK_Lesson_Grade");

            builder.HasOne(d => d.LessonOwner)
                .WithMany(p => p.LessonLessonOwners)
                .HasForeignKey(d => d.LessonOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lesson_User");

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.Lessons)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_Lesson_Subject");

            builder.HasOne(d => d.UpdatedBy)
                .WithMany(p => p.LessonUpdatedBies)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lesson_User2");
        }
    }
}
