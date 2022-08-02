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
    public class LessonLectureConfiguration : IEntityTypeConfiguration<LessonLecture>
    {
        public void Configure(EntityTypeBuilder<LessonLecture> builder)
        {
            builder.ToTable("LessonLecture");

            builder.Property(e => e.Mimetype)
                .HasMaxLength(50)
                .HasColumnName("MIMEType");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(d => d.LectureContentType)
                .WithMany(p => p.LessonLectures)
                .HasForeignKey(d => d.LectureContentTypeId)
                .HasConstraintName("FK_LessonLecture_LessonLectureContentType");

            builder.HasOne(d => d.Topic)
                .WithMany(p => p.LessonLectures)
                .HasForeignKey(d => d.TopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonLecture_LessonTopic");
        }
    }
}
