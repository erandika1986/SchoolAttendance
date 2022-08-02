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
    public class SubjectAttendanceConfiguration : IEntityTypeConfiguration<SubjectAttendance>
    {
        public void Configure(EntityTypeBuilder<SubjectAttendance> builder)
        {
            builder.ToTable("SubjectAttendance");

            builder.Property(e => e.ActualEnteredDate).HasColumnType("datetime");

            builder.Property(e => e.Date).HasColumnType("datetime");

            builder.Property(e => e.EndTime).HasColumnType("datetime");

            builder.Property(e => e.StartTime).HasColumnType("datetime");

            builder.Property(e => e.UsedSoftwareName).HasMaxLength(50);

            builder.HasOne(d => d.Class)
                .WithMany(p => p.SubjectAttendances)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Class");

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.SubjectAttendances)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Subject");

            builder.HasOne(d => d.TimeSlot)
                .WithMany(p => p.SubjectAttendances)
                .HasForeignKey(d => d.TimeSlotId)
                .HasConstraintName("FK_SubjectAttendance_ClassSubjectTimeTable");
        }
    }
}
