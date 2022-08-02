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
    public class AssessmentSectionQuestionConfiguration : IEntityTypeConfiguration<AssessmentSectionQuestion>
    {
        public void Configure(EntityTypeBuilder<AssessmentSectionQuestion> builder)
        {
            builder.ToTable("AssessmentSectionQuestion");

            builder.Property(e => e.Score).HasColumnType("decimal(6, 2)");

            builder.HasOne(d => d.AssessementSection)
                .WithMany(p => p.AssessmentSectionQuestions)
                .HasForeignKey(d => d.AssessementSectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentSectionQuestion_AssessmentSection");

            builder.HasOne(d => d.Question)
                .WithMany(p => p.AssessmentSectionQuestions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentSectionQuestion_Question");
        }
    }
}
