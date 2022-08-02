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
    public class LessonUnitTestTopicConfiguration : IEntityTypeConfiguration<LessonUnitTestTopic>
    {
        public void Configure(EntityTypeBuilder<LessonUnitTestTopic> builder)
        {
            builder.ToTable("LessonUnitTestTopic");

            builder.Property(e => e.Instruction).IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(d => d.LessonUnitTest)
                .WithMany(p => p.LessonUnitTestTopics)
                .HasForeignKey(d => d.LessonUnitTestId)
                .HasConstraintName("FK_LessonUnitTestTopic_LessonUnitTest");

            builder.HasOne(d => d.QuestionType)
                .WithMany(p => p.LessonUnitTestTopics)
                .HasForeignKey(d => d.QuestionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonUnitTestTopic_QuestionType");
        }
    }
}
