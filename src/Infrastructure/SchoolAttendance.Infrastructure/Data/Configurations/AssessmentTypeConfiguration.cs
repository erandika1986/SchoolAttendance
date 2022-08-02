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
    public class AssessmentTypeConfiguration : IEntityTypeConfiguration<AssessmentType>
    {
        public void Configure(EntityTypeBuilder<AssessmentType> builder)
        {
            builder.ToTable("AssessmentType");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
