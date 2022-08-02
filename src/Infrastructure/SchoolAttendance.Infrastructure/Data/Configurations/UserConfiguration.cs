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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasIndex(e => e.Username, "UQ__User__536C85E4FCCFF6D0")
                .IsUnique();

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.TimeZoneId).HasMaxLength(1000);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
