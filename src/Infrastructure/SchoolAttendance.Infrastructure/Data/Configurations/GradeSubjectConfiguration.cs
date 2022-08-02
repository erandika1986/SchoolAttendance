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
    public class GradeSubjectConfiguration : IEntityTypeConfiguration<GradeSubject>
    {
        public void Configure(EntityTypeBuilder<GradeSubject> builder)
        {
            builder.HasKey(e => new { e.SubjectId, e.GradeId });

            builder.ToTable("GradeSubject");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Grade)
                .WithMany(p => p.GradeSubjects)
                .HasForeignKey(d => d.GradeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GradeSubject_Grade");

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.GradeSubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GradeSubject_Subject");
        }
    }
}
