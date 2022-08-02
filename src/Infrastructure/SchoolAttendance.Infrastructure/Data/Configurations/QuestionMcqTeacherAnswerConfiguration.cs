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
    public class QuestionMcqTeacherAnswerConfiguration : IEntityTypeConfiguration<QuestionMcqteacherAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionMcqteacherAnswer> builder)
        {
            builder.ToTable("QuestionMCQTeacherAnswer");

            builder.Property(e => e.AnswerText).IsRequired();

            builder.Property(e => e.AnswerTextRt)
                .IsRequired()
                .HasColumnName("AnswerTextRT");

            builder.Property(e => e.SequenceNo).HasColumnName("SequenceNO");

            builder.HasOne(d => d.Question)
                .WithMany(p => p.QuestionMcqteacherAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionMCQTeacherAnswer_Question");
        }
    }
}
