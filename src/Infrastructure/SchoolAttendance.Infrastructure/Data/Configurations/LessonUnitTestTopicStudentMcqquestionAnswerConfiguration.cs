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
    public class LessonUnitTestTopicStudentMcqquestionAnswerConfiguration : IEntityTypeConfiguration<LessonUnitTestTopicStudentMcqquestionAnswer>
    {
        public void Configure(EntityTypeBuilder<LessonUnitTestTopicStudentMcqquestionAnswer> builder)
        {
            builder.ToTable("LessonUnitTestTopicStudentMCQQuestionAnswer");

            builder.HasOne(d => d.LessonUnitTestTopicStudentQuestion)
                .WithMany(p => p.LessonUnitTestTopicStudentMcqquestionAnswers)
                .HasForeignKey(d => d.LessonUnitTestTopicStudentQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonUnitTestTopicStudentMCQQuestionAnswer_LessonUnitTestTopicStudentQuestion");

            builder.HasOne(d => d.TeacherAnswer)
                .WithMany(p => p.LessonUnitTestTopicStudentMcqquestionAnswers)
                .HasForeignKey(d => d.TeacherAnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonUnitTestTopicStudentMCQQuestionAnswer_QuestionMCQTeacherAnswer");
        }
    }
}
