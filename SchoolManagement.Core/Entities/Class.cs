using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Entities
{
    public class Class: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int CurrentStrength { get; set; }
        public int? ClassTeacherId { get; set; }

        // Navigation properties
        public virtual User? ClassTeacher { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public virtual ICollection<StudentEnrollment> StudentEnrollments { get; set; } = new List<StudentEnrollment>();
        public virtual ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();

    }
}
