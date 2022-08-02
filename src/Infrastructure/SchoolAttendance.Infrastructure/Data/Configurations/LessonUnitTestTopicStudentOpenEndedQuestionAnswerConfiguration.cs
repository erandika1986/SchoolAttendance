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
    public class LessonUnitTestTopicStudentOpenEndedQuestionAnswerConfiguration : IEntityTypeConfiguration<LessonUnitTestTopicStudentOpenEndedQuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<LessonUnitTestTopicStudentOpenEndedQuestionAnswer> builder)
        {
            builder.ToTable("LessonUnitTestTopicStudentOpenEndedQuestionAnswer");

            builder.Property(e => e.AnswerRt).HasColumnName("AnswerRT");

            builder.HasOne(d => d.LessonUnitTestTopicStudentQuestion)
                .WithMany(p => p.LessonUnitTestTopicStudentOpenEndedQuestionAnswers)
                .HasForeignKey(d => d.LessonUnitTestTopicStudentQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonUnitTestTopicStudentOpenEndedQuestionAnswer_LessonUnitTestTopicStudentQuestion");
        }
    }
}
