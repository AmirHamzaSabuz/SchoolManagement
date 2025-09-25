using SchoolManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Interfaces
{
    public interface IStudentEnrollmentRepository : IGenericRepository<StudentEnrollment>
    {
        Task<StudentEnrollment?> GetByStudentIdAndClassIdAsync(int studentId, int classId);
        Task<IReadOnlyList<StudentEnrollment>> GetEnrollmentsByClassAsync(int classId);
        Task<IReadOnlyList<StudentEnrollment>> GetEnrollmentsByStudentAsync(int studentId);
        Task<string> GenerateRollNumberAsync(int classId);
    }
}
