using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Entities
{
    public class Grade: BaseEntity
    {
        public int StudentEnrollmentId { get; set; }
        public int SubjectId { get; set; }
        public string ExamType { get; set; } = string.Empty; // Midterm, Final, Quiz, Assignment
        public decimal MarksObtained { get; set; }
        public decimal TotalMarks { get; set; }
        public DateTime ExamDate { get; set; }
        public string? Remarks { get; set; }

        // Navigation properties
        public virtual StudentEnrollment StudentEnrollment { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
