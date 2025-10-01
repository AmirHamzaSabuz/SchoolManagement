using Microsoft.EntityFrameworkCore.Storage;
using SchoolManagement.Core.Entities;
using SchoolManagement.Core.Interfaces;
using SchoolManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(SchoolDbContext context)
        {
            _context = context;
        }

        private IUserRepository? _userRepository;
        private IClassRepository? _classRepository;
        private IGenericRepository<Subject>? _subjectRepository;
        private IStudentEnrollmentRepository? _studentEnrollmentRepository;
        private IGenericRepository<TeacherAssignment>? _teacherAssignmentRepository;
        private IGenericRepository<Grade>? _gradeRepository;
        private IGenericRepository<Attendance>? _attendanceRepository;

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public IClassRepository Classes => _classRepository ??= new ClassRepository(_context);
        public IGenericRepository<Subject> Subjects => _subjectRepository ??= new GenericRepository<Subject>(_context);
        public IStudentEnrollmentRepository StudentEnrollments => _studentEnrollmentRepository ??= new StudentEnrollmentRepository(_context);
        public IGenericRepository<TeacherAssignment> TeacherAssignments => _teacherAssignmentRepository ??= new GenericRepository<TeacherAssignment>(_context);
        public IGenericRepository<Grade> Grades => _gradeRepository ??= new GenericRepository<Grade>(_context);
        public IGenericRepository<Attendance> Attendances => _attendanceRepository ??= new GenericRepository<Attendance>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
