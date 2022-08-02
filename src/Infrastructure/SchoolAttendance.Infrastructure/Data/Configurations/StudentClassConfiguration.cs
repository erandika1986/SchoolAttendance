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
    public class StudentClassConfiguration : IEntityTypeConfiguration<StudentClass>
    {
        public void Configure(EntityTypeBuilder<StudentClass> builder)
        {
            builder.HasKey(e => new { e.ClassId, e.StudentId });

            builder.ToTable("StudentClass");

            builder.Property(e => e.AssignedDate).HasColumnType("datetime");

            builder.Property(e => e.RemovedDate).HasColumnType("datetime");

            builder.HasOne(d => d.Class)
                .WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentClass_Class");

            builder.HasOne(d => d.Student)
                .WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentClass_User");
        }
    }
}
