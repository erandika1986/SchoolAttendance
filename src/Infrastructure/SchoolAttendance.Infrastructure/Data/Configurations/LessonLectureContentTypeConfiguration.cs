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
    public class LessonLectureContentTypeConfiguration : IEntityTypeConfiguration<LessonLectureContentType>
    {
        public void Configure(EntityTypeBuilder<LessonLectureContentType> builder)
        {
            builder.ToTable("LessonLectureContentType");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
