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
    public class StudentEnrollmentConfiguration : IEntityTypeConfiguration<StudentEnrollment>
    {
        public void Configure(EntityTypeBuilder<StudentEnrollment> builder)
        {
            builder.ToTable("StudentEnrollments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RollNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.RollNumber).IsUnique();

            builder.Property(x => x.AcademicYear)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.BloodGroup)
                .HasConversion<int>();

            builder.HasOne(x => x.Student)
                .WithMany(x => x.StudentEnrollments)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Class)
                .WithMany(x => x.StudentEnrollments)
                .HasForeignKey(x => x.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
