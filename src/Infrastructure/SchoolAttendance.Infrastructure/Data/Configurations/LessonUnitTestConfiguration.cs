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
    public class LessonUnitTestConfiguration : IEntityTypeConfiguration<LessonUnitTest>
    {
        public void Configure(EntityTypeBuilder<LessonUnitTest> builder)
        {
            builder.ToTable("LessonUnitTest");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.StudentGuide).IsRequired();

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.LessonUnitTests)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonUnitTest_Lesson");
        }
    }
}
