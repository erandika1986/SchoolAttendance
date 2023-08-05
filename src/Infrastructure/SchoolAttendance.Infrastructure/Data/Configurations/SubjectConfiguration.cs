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
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subject");

            builder.HasIndex(e => e.Name, "UQ__Subject__737584F6FE2978E8")
                .IsUnique();

            builder.Property(e => e.Description).HasMaxLength(500);

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.IsParentSubject)
                .IsRequired()
                .HasDefaultValueSql("((0))");

            builder.Property(e => e.Medium).HasMaxLength(20);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.DepartmentHead)
                .WithMany(p => p.Subjects)
                .HasForeignKey(d => d.DepartmentHeadId)
                .HasConstraintName("FK_Subject_User");

            builder.HasOne(d => d.ParentSubject)
                .WithMany(p => p.InverseParentSubject)
                .HasForeignKey(d => d.ParentSubjectId)
                .HasConstraintName("FK_Subject_Subject");
        }
    }
}
