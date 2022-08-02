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
    public class AssessmentMcqquestionStudentAnswerConfiguration : IEntityTypeConfiguration<AssessmentMcqquestionStudentAnswer>
    {
        public void Configure(EntityTypeBuilder<AssessmentMcqquestionStudentAnswer> builder)
        {
            builder.ToTable("AssessmentMCQQuestionStudentAnswer");

            builder.Property(e => e.SubmittedOn).HasColumnType("datetime");

            builder.HasOne(d => d.AssessmentSectionStudentQuestion)
                .WithMany(p => p.AssessmentMcqquestionStudentAnswers)
                .HasForeignKey(d => d.AssessmentSectionStudentQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentMCQQuestionStudentAnswer_AssessmentSectionStudentQuestion");

            builder.HasOne(d => d.TeacherAnswer)
                .WithMany(p => p.AssessmentMcqquestionStudentAnswers)
                .HasForeignKey(d => d.TeacherAnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentMCQQuestionStudentAnswer_QuestionMCQTeacherAnswer");
        }
    }
}
