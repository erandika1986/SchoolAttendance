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
    public class ClassSubjectTimeTableConfiguration : IEntityTypeConfiguration<ClassSubjectTimeTable>
    {
        public void Configure(EntityTypeBuilder<ClassSubjectTimeTable> builder)
        {
            builder.ToTable("ClassSubjectTimeTable");

            builder.Property(e => e.EndTime).HasColumnType("datetime");

            builder.Property(e => e.StartTime).HasColumnType("datetime");

            builder.HasOne(d => d.Class)
                .WithMany(p => p.ClassSubjectTimeTables)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassSubjectTimeTable_Class");

            builder.HasOne(d => d.Day)
                .WithMany(p => p.ClassSubjectTimeTables)
                .HasForeignKey(d => d.DayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassSubjectTimeTable_Days");

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.ClassSubjectTimeTables)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassSubjectTimeTable_Subject");
        }
    }
}
