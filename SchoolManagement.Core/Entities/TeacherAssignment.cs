using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Entities
{
    public class TeacherAssignment : BaseEntity
    {
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public string AcademicYear { get; set; } = string.Empty;

        // Navigation properties
        public virtual User Teacher { get; set; } = null!;
        public virtual Class Class { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
