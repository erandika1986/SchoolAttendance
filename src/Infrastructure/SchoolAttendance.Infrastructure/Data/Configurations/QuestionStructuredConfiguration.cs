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
    public class QuestionStructuredConfiguration : IEntityTypeConfiguration<QuestionStructured>
    {
        public void Configure(EntityTypeBuilder<QuestionStructured> builder)
        {
            builder.ToTable("QuestionStructured");

            builder.Property(e => e.QuestionText).IsRequired();

            builder.Property(e => e.QuestionTextRt)
                .IsRequired()
                .HasColumnName("QuestionTextRT");

            builder.HasOne(d => d.Question)
                .WithMany(p => p.QuestionStructureds)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionStructured_Question");
        }
    }
}
