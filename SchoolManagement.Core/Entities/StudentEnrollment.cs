using SchoolManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Entities
{
    public class StudentEnrollment : BaseEntity
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public string RollNumber { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public BloodGroup BloodGroup { get; set; }
        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;

        // Navigation properties
        public virtual User Student { get; set; } = null!;
        public virtual Class Class { get; set; } = null!;
        public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
