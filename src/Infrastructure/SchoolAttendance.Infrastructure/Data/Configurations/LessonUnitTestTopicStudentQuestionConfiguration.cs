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
    public class LessonUnitTestTopicStudentQuestionConfiguration : IEntityTypeConfiguration<LessonUnitTestTopicStudentQuestion>
    {
        public void Configure(EntityTypeBuilder<LessonUnitTestTopicStudentQuestion> builder)
        {
            builder.ToTable("LessonUnitTestTopicStudentQuestion");

            builder.Property(e => e.Score).HasColumnType("decimal(6, 2)");

            builder.HasOne(d => d.LessonUnitTestTopicQuestion)
                .WithMany(p => p.LessonUnitTestTopicStudentQuestions)
                .HasForeignKey(d => d.LessonUnitTestTopicQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonUnitTestTopicStudentQuestion_LessonUnitTestTopicQuestion");

            builder.HasOne(d => d.Student)
                .WithMany(p => p.LessonUnitTestTopicStudentQuestions)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonUnitTestTopicStudentQuestion_User");
        }
    }
}
