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
    public class AssessmentClassStudentConfiguration : IEntityTypeConfiguration<AssessmentClassStudent>
    {
        public void Configure(EntityTypeBuilder<AssessmentClassStudent> builder)
        {
            builder.HasKey(e => new { e.AssessmentId, e.StudentId, e.ClassId });

            builder.ToTable("AssessmentClassStudent");

            builder.Property(e => e.CompletedOn).HasColumnType("datetime");

            builder.Property(e => e.ConnectedBrowser).HasMaxLength(500);

            builder.Property(e => e.ConnectedIp)
                .HasMaxLength(50)
                .HasColumnName("ConnectedIP");

            builder.Property(e => e.Score).HasColumnType("decimal(6, 2)");

            builder.Property(e => e.ScorePrecentaged).HasColumnType("decimal(6, 2)");

            builder.Property(e => e.StartedOn).HasColumnType("datetime");

            builder.HasOne(d => d.Assessment)
                .WithMany(p => p.AssessmentClassStudents)
                .HasForeignKey(d => d.AssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentClassStudent_Assessment");

            builder.HasOne(d => d.Class)
                .WithMany(p => p.AssessmentClassStudents)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentClassStudent_Class1");

            builder.HasOne(d => d.Student)
                .WithMany(p => p.AssessmentClassStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentClassStudent_User");
        }
    }
}
