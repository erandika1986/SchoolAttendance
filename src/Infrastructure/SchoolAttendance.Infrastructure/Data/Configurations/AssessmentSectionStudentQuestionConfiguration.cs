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
    public class AssessmentSectionStudentQuestionConfiguration : IEntityTypeConfiguration<AssessmentSectionStudentQuestion>
    {
        public void Configure(EntityTypeBuilder<AssessmentSectionStudentQuestion> builder)
        {
            builder.ToTable("AssessmentSectionStudentQuestion");

            builder.Property(e => e.Score).HasColumnType("decimal(6, 2)");

            builder.HasOne(d => d.AssessmentSectionQuestion)
                .WithMany(p => p.AssessmentSectionStudentQuestions)
                .HasForeignKey(d => d.AssessmentSectionQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentSectionStudentQuestion_AssessmentSectionQuestion");

            builder.HasOne(d => d.Student)
                .WithMany(p => p.AssessmentSectionStudentQuestions)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentSectionStudentQuestion_User");
        }
    }
}
