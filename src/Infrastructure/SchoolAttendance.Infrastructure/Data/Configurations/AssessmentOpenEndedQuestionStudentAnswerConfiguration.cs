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
    public class AssessmentOpenEndedQuestionStudentAnswerConfiguration : IEntityTypeConfiguration<AssessmentOpenEndedQuestionStudentAnswer>
    {
        public void Configure(EntityTypeBuilder<AssessmentOpenEndedQuestionStudentAnswer> builder)
        {
            builder.ToTable("AssessmentOpenEndedQuestionStudentAnswer");

            builder.Property(e => e.AnswerText).IsRequired();

            builder.Property(e => e.AnswerTextRt)
                .IsRequired()
                .HasColumnName("AnswerTextRT");

            builder.Property(e => e.SubmittedOn).HasColumnType("datetime");

            builder.HasOne(d => d.AssessmentSectionStudentQuestion)
                .WithMany(p => p.AssessmentOpenEndedQuestionStudentAnswers)
                .HasForeignKey(d => d.AssessmentSectionStudentQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentOpenEndedQuestionStudentAnswer_AssessmentSectionStudentQuestion");
        }
    }
}
