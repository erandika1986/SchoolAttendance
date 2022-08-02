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
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");

            builder.Property(e => e.Question1)
                .IsRequired()
                .HasColumnName("Question");

            builder.Property(e => e.QuestionRt)
                .IsRequired()
                .HasColumnName("QuestionRT");

            builder.HasOne(d => d.AcademicYear)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.AcademicYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_AcademicYear");

            builder.HasOne(d => d.Grade)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.GradeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_Grade");

            builder.HasOne(d => d.Owner)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_User");

            builder.HasOne(d => d.Subject)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_Subject");
        }
    }
}
