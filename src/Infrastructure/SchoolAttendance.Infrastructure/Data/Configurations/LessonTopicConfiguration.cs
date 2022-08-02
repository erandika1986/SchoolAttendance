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
    public class LessonTopicConfiguration : IEntityTypeConfiguration<LessonTopic>
    {
        public void Configure(EntityTypeBuilder<LessonTopic> builder)
        {
            builder.ToTable("LessonTopic");

            builder.Property(e => e.Name).HasMaxLength(1000);

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.LessonTopics)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("FK_LessonTopic_Lesson");
        }
    }
}
