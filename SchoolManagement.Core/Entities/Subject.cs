using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int ClassId { get; set; }

        // Navigation properties
        public virtual Class Class { get; set; } = null!;
        public virtual ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
        public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
