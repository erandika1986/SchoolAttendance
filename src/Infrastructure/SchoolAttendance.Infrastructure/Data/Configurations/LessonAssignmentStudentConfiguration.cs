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
    public class LessonAssignmentStudentConfiguration : IEntityTypeConfiguration<LessonAssignmentStudent>
    {
        public void Configure(EntityTypeBuilder<LessonAssignmentStudent> builder)
        {
            builder.ToTable("LessonAssignmentStudent");

            builder.Property(e => e.Score).HasColumnType("decimal(6, 2)");

            builder.Property(e => e.StudentBrowser).HasMaxLength(500);

            builder.Property(e => e.StudentIp)
                .HasMaxLength(50)
                .HasColumnName("StudentIP");

            builder.Property(e => e.SubmittedOn).HasColumnType("datetime");

            builder.HasOne(d => d.LessonAssignment)
                .WithMany(p => p.LessonAssignmentStudents)
                .HasForeignKey(d => d.LessonAssignmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonAssignmentStudent_LessonAssignment");

            builder.HasOne(d => d.Student)
                .WithMany(p => p.LessonAssignmentStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonAssignmentStudent_User");
        }
    }
}
