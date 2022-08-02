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
    public class StudentAssessmentScoreConfiguration : IEntityTypeConfiguration<StudentAssessmentScore>
    {
        public void Configure(EntityTypeBuilder<StudentAssessmentScore> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.AssessmentId });

            builder.ToTable("StudentAssessmentScore");

            builder.Property(e => e.ActualScore).HasColumnType("decimal(6, 2)");

            builder.Property(e => e.ActualScoreEnteredOn).HasColumnType("datetime");

            builder.Property(e => e.PredictedTargetScore).HasColumnType("decimal(6, 2)");

            builder.Property(e => e.TargetAdjustedOn).HasColumnType("datetime");

            builder.Property(e => e.TargetGeneratedOn).HasColumnType("datetime");

            builder.Property(e => e.TeacherAdjustedTargetScore).HasColumnType("decimal(6, 2)");

            builder.HasOne(d => d.Assessment)
                .WithMany(p => p.StudentAssessmentScores)
                .HasForeignKey(d => d.AssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentAssessmentTarget_Assessment");

            builder.HasOne(d => d.Student)
                .WithMany(p => p.StudentAssessmentScores)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentAssessmentTarget_User");
        }
    }
}
