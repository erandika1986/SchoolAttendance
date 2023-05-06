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
    public class QuestionTructuredTeacherAnswerConfiguration : IEntityTypeConfiguration<QuestionStructuredTeacherAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionStructuredTeacherAnswer> builder)
        {
            builder.ToTable("QuestionTructuredTeacherAnswer");

            builder.Property(e => e.AnswerTextRt).HasColumnName("AnswerTextRT");

            builder.HasOne(d => d.QuestionStructured)
                .WithMany(p => p.QuestionTructuredTeacherAnswers)
                .HasForeignKey(d => d.QuestionStructuredId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionTructuredTeacherAnswer_QuestionStructured");
        }
    }
}
