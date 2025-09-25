using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Entities
{
    public class Attendance : BaseEntity
    {
        public int StudentEnrollmentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public string? Remarks { get; set; }

        // Navigation properties
        public virtual StudentEnrollment StudentEnrollment { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
