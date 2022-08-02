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
    public class QuestionOpenEndedTeacherAnswerConfiguration : IEntityTypeConfiguration<QuestionOpenEndedTeacherAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionOpenEndedTeacherAnswer> builder)
        {
            builder.ToTable("QuestionOpenEndedTeacherAnswer");

            builder.Property(e => e.AnswerTextRt).HasColumnName("AnswerTextRT");

            builder.HasOne(d => d.Question)
                .WithMany(p => p.QuestionOpenEndedTeacherAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionOpenEndedTeacherAnswer_Question");
        }
    }
}
