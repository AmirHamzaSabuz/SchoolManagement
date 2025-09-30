using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Data.Config
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendances");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .IsRequired()
                .HasColumnType("date"); // Store only date part, not time

            builder.Property(x => x.IsPresent)
                .IsRequired();

            builder.Property(x => x.Remarks)
                .HasMaxLength(500);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Unique constraint to prevent duplicate attendance records for same student, subject, and date
            builder.HasIndex(x => new { x.StudentEnrollmentId, x.SubjectId, x.Date })
                .IsUnique()
                .HasDatabaseName("IX_Attendances_Student_Subject_Date_Unique");

            // Index for date-based queries
            builder.HasIndex(x => x.Date)
                .HasDatabaseName("IX_Attendances_Date");

            // Relationships
            builder.HasOne(x => x.StudentEnrollment)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.StudentEnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Subject)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
