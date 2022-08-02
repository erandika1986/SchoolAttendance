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
    public class SubjectTeacherConfiguration : IEntityTypeConfiguration<SubjectTeacher>
    {
        public void Configure(EntityTypeBuilder<SubjectTeacher> builder)
        {
            builder.Property(e => e.AssignedDate).HasColumnType("datetime");

            builder.Property(e => e.DeAllocatedDate).HasColumnType("datetime");

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.SubjectTeachers)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubjectTeachers_Subject");

            builder.HasOne(d => d.Teacher)
                .WithMany(p => p.SubjectTeachers)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubjectTeachers_User");
        }
    }
}
