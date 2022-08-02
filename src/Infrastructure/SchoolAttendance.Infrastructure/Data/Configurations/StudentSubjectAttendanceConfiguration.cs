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
    public class StudentSubjectAttendanceConfiguration : IEntityTypeConfiguration<StudentSubjectAttendance>
    {
        public void Configure(EntityTypeBuilder<StudentSubjectAttendance> builder)
        {
            builder.HasKey(e => new { e.SubjectAttendanceId, e.StudentId });

            builder.ToTable("StudentSubjectAttendance");

            builder.HasOne(d => d.Student)
                .WithMany(p => p.StudentSubjectAttendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentSubjectAttendance_User");

            builder.HasOne(d => d.SubjectAttendance)
                .WithMany(p => p.StudentSubjectAttendances)
                .HasForeignKey(d => d.SubjectAttendanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentSubjectAttendance_SubjectAttendance");
        }
    }
}
