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
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades", t =>
            {
                // Check constraint to ensure marks obtained don't exceed total marks and are non-negative
                t.HasCheckConstraint("CK_Grades_MarksObtained_Valid",
                    "[MarksObtained] >= 0 AND [MarksObtained] <= [TotalMarks]");

                // Check constraint to ensure total marks are positive
                t.HasCheckConstraint("CK_Grades_TotalMarks_Positive",
                    "[TotalMarks] > 0");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExamType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.MarksObtained)
                .IsRequired()
                .HasPrecision(6, 2); // Allows values up to 9999.99

            builder.Property(x => x.TotalMarks)
                .IsRequired()
                .HasPrecision(6, 2); // Allows values up to 9999.99

            builder.Property(x => x.ExamDate)
                .IsRequired()
                .HasColumnType("date");

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

            // Index for better query performance
            builder.HasIndex(x => new { x.StudentEnrollmentId, x.SubjectId, x.ExamType })
                .HasDatabaseName("IX_Grades_Student_Subject_ExamType");

            // Relationships
            builder.HasOne(x => x.StudentEnrollment)
                .WithMany(x => x.Grades)
                .HasForeignKey(x => x.StudentEnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Subject)
                .WithMany(x => x.Grades)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
