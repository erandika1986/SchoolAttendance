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
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Class");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.AcademicYearNavigation)
                .WithMany(p => p.Classes)
                .HasForeignKey(d => d.AcademicYear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_AcademicYear");

            builder.HasOne(d => d.ClassTeacher)
                .WithMany(p => p.Classes)
                .HasForeignKey(d => d.ClassTeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_User");

            builder.HasOne(d => d.Grade)
                .WithMany(p => p.Classes)
                .HasForeignKey(d => d.GradeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_Grade");
        }
    }
}
