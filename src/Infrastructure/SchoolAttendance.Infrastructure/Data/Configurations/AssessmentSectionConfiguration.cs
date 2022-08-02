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
    public class AssessmentSectionConfiguration : IEntityTypeConfiguration<AssessmentSection>
    {
        public void Configure(EntityTypeBuilder<AssessmentSection> builder)
        {
            builder.ToTable("AssessmentSection");

            builder.Property(e => e.Instructions).IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(d => d.Assessment)
                .WithMany(p => p.AssessmentSections)
                .HasForeignKey(d => d.AssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentSection_Assessment");
        }
    }
}
