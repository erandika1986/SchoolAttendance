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
    public class AssessmentClassConfiguration : IEntityTypeConfiguration<AssessmentClass>
    {
        public void Configure(EntityTypeBuilder<AssessmentClass> builder)
        {
            builder.HasKey(e => new { e.ClassId, e.AssessmentId });

            builder.ToTable("AssessmentClass");

            builder.Property(e => e.PublishedOn).HasColumnType("datetime");

            builder.HasOne(d => d.Assessment)
                .WithMany(p => p.AssessmentClasses)
                .HasForeignKey(d => d.AssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentClass_Assessment");

            builder.HasOne(d => d.Class)
                .WithMany(p => p.AssessmentClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssessmentClass_Class");
        }
    }
}
