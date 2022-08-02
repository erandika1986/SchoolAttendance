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
    public class LessonAssignmentConfiguration : IEntityTypeConfiguration<LessonAssignment>
    {
        public void Configure(EntityTypeBuilder<LessonAssignment> builder)
        {
            builder.ToTable("LessonAssignment");

            builder.Property(e => e.ClosingDateTime)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength(true);

            builder.Property(e => e.Instruction).IsRequired();

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(d => d.Lesson)
                .WithMany(p => p.LessonAssignments)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonAssignment_Lesson");
        }
    }
}
