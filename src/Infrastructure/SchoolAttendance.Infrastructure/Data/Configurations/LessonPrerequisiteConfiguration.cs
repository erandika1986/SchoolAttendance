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
    public class LessonPrerequisiteConfiguration : IEntityTypeConfiguration<LessonPrerequisite>
    {
        public void Configure(EntityTypeBuilder<LessonPrerequisite> builder)
        {
            builder.Property(e => e.Prerequisite)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.LessonPrerequisites)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonPrerequisites_Lesson");
        }
    }
}
