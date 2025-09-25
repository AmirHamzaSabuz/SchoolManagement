using SchoolManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IClassRepository Classes { get; }
        IGenericRepository<Subject> Subjects { get; }
        IStudentEnrollmentRepository StudentEnrollments { get; }
        IGenericRepository<TeacherAssignment> TeacherAssignments { get; }
        IGenericRepository<Grade> Grades { get; }
        IGenericRepository<Attendance> Attendances { get; }

        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
