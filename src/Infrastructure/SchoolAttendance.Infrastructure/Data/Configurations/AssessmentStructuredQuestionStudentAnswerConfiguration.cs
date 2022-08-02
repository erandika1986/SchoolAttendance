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
    public class AssessmentStructuredQuestionStudentAnswerConfiguration : IEntityTypeConfiguration<AssessmentStructuredQuestionStudentAnswer>
    {
        public void Configure(EntityTypeBuilder<AssessmentStructuredQuestionStudentAnswer> builder)
        {
            builder.ToTable("AssessmentStructuredQuestionStudentAnswer");

            builder.Property(e => e.AnswerText).IsRequired();

            builder.Property(e => e.AnswerTextRt)
                .IsRequired()
                .HasColumnName("AnswerTextRT");

            builder.Property(e => e.SubmittedOn).HasColumnType("datetime");

            builder.HasOne(d => d.AssessmentSectionStudentQuestion)
                .WithMany(p => p.AssessmentStructuredQuestionStudentAnswers)
                .HasForeignKey(d => d.AssessmentSectionStudentQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentStructuredQuestionStudentAnswer_AssessmentSectionStudentQuestion");

            builder.HasOne(d => d.StructuredQuestion)
                .WithMany(p => p.AssessmentStructuredQuestionStudentAnswers)
                .HasForeignKey(d => d.StructuredQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentStructuredQuestionStudentAnswer_QuestionStructured");
        }
    }
}
