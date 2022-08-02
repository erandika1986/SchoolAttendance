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
    public class LessonAssignmentStudentUploadConfiguration : IEntityTypeConfiguration<LessonAssignmentStudentUpload>
    {
        public void Configure(EntityTypeBuilder<LessonAssignmentStudentUpload> builder)
        {
            builder.ToTable("LessonAssignmentStudentUpload");

            builder.Property(e => e.UploadFilePath).IsRequired();

            builder.Property(e => e.UploadedOn).HasColumnType("datetime");

            builder.HasOne(d => d.LessonAssignmentStudent)
                .WithMany(p => p.LessonAssignmentStudentUploads)
                .HasForeignKey(d => d.LessonAssignmentStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonAssignmentStudentUpload_LessonAssignmentStudent");
        }
    }
}
