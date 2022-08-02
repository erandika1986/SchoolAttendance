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
    public class LessonUnitTestTopicQuestionConfiguration : IEntityTypeConfiguration<LessonUnitTestTopicQuestion>
    {
        public void Configure(EntityTypeBuilder<LessonUnitTestTopicQuestion> builder)
        {
            builder.ToTable("LessonUnitTestTopicQuestion");

            builder.Property(e => e.Score).HasColumnType("decimal(6, 2)");

            builder.HasOne(d => d.LessonUnitTestTopic)
                .WithMany(p => p.LessonUnitTestTopicQuestions)
                .HasForeignKey(d => d.LessonUnitTestTopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonUnitTestTopicQuestion_LessonUnitTestTopic");

            builder.HasOne(d => d.Question)
                .WithMany(p => p.LessonUnitTestTopicQuestions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonUnitTestTopicQuestion_Question");
        }
    }
}
