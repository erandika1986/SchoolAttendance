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
    public class ClassSubjectStudentConfiguration : IEntityTypeConfiguration<ClassSubjectStudent>
    {
        public void Configure(EntityTypeBuilder<ClassSubjectStudent> builder)
        {
            builder.HasKey(e => new { e.ClassId, e.SubjectId, e.StudentId });

            builder.ToTable("ClassSubjectStudent");

            builder.Property(e => e.AssignedDate).HasColumnType("datetime");

            builder.Property(e => e.DeAllocatedDate).HasColumnType("datetime");
        }
    }
}
