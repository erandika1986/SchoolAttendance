using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Data.Configurations
{
    public class AssessmentUploadConfiguration : IEntityTypeConfiguration<AssessmentUpload>
    {
        public void Configure(EntityTypeBuilder<AssessmentUpload> builder)
        {
            builder.ToTable("AssessmentUpload");

            builder.HasOne(d => d.Assessment)
                .WithMany(p => p.AssessmentUploads)
                .HasForeignKey(d => d.AssessmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
