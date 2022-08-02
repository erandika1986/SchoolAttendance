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
    public class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.ToTable("Assessment");

            builder.Property(e => e.CompletedOn).HasColumnType("datetime");

            builder.Property(e => e.CreatedOn).HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.PublishedOn).HasColumnType("datetime");

            builder.Property(e => e.UpdatedOn).HasColumnType("datetime");

            builder.HasOne(d => d.AcademicYear)
                .WithMany(p => p.Assessments)
                .HasForeignKey(d => d.AcademicYearId)
                .HasConstraintName("FK_Assessment_AcademicYear");

            builder.HasOne(d => d.ApprovedByNavigation)
                .WithMany(p => p.AssessmentApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_Assessment_User2");

            builder.HasOne(d => d.AssessmentType)
                .WithMany(p => p.Assessments)
                .HasForeignKey(d => d.AssessmentTypeId)
                .HasConstraintName("FK_Assessment_AssessmentType");

            builder.HasOne(d => d.CreatedBy)
                .WithMany(p => p.AssessmentCreatedBies)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assessment_User");

            builder.HasOne(d => d.Grade)
                .WithMany(p => p.Assessments)
                .HasForeignKey(d => d.GradeId)
                .HasConstraintName("FK_Assessment_Grade");

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.Assessments)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_Assessment_Subject");

            builder.HasOne(d => d.UpdatedBy)
                .WithMany(p => p.AssessmentUpdatedBies)
                .HasForeignKey(d => d.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assessment_User1");
        }
    }
}
