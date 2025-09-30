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
    public class TeacherAssignmentConfiguration : IEntityTypeConfiguration<TeacherAssignment>
    {
        public void Configure(EntityTypeBuilder<TeacherAssignment> builder)
        {
            builder.ToTable("TeacherAssignments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AcademicYear)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Ensure a teacher can't be assigned to the same subject in the same class for the same academic year
            builder.HasIndex(x => new { x.TeacherId, x.ClassId, x.SubjectId, x.AcademicYear })
                .IsUnique()
                .HasDatabaseName("IX_TeacherAssignments_Unique");

            // Relationships
            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.TeacherAssignments)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Class)
                .WithMany(x => x.TeacherAssignments)
                .HasForeignKey(x => x.ClassId)
                .OnDelete(DeleteBehavior.NoAction); // Changed to NoAction to avoid multiple cascade paths

            builder.HasOne(x => x.Subject)
                .WithMany(x => x.TeacherAssignments)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }


}
