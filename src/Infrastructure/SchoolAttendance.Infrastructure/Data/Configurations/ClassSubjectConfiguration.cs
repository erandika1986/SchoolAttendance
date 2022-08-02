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
    public class ClassSubjectConfiguration : IEntityTypeConfiguration<ClassSubject>
    {
        public void Configure(EntityTypeBuilder<ClassSubject> builder)
        {
            builder.HasKey(e => new { e.ClassId, e.SubjectId });

            builder.ToTable("ClassSubject");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Class)
                .WithMany(p => p.ClassSubjects)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassSubject_Class");

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.ClassSubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassSubject_Subject");

            builder.HasOne(d => d.SubjectTeacher)
                .WithMany(p => p.ClassSubjects)
                .HasForeignKey(d => d.SubjectTeacherId)
                .HasConstraintName("FK_ClassSubject_User");
        }
    }
}
