using Microsoft.EntityFrameworkCore;
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
    public class StudentEnrollmentRepository : GenericRepository<StudentEnrollment>, IStudentEnrollmentRepository
    {
        public StudentEnrollmentRepository(SchoolDbContext context) : base(context) { }

        public async Task<StudentEnrollment?> GetByStudentIdAndClassIdAsync(int studentId, int classId)
        {
            return await _dbSet
                .Include(se => se.Student)
                .Include(se => se.Class)
                .FirstOrDefaultAsync(se => se.StudentId == studentId && se.ClassId == classId);
        }

        public async Task<IReadOnlyList<StudentEnrollment>> GetEnrollmentsByClassAsync(int classId)
        {
            return await _dbSet
                .Include(se => se.Student)
                .Where(se => se.ClassId == classId && se.IsActive)
                .OrderBy(se => se.RollNumber)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<StudentEnrollment>> GetEnrollmentsByStudentAsync(int studentId)
        {
            return await _dbSet
                .Include(se => se.Class)
                .Where(se => se.StudentId == studentId && se.IsActive)
                .ToListAsync();
        }

        public async Task<string> GenerateRollNumberAsync(int classId)
        {
            var className = await _context.Classes
                .Where(c => c.Id == classId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();

            var currentYear = DateTime.Now.Year.ToString();
            var lastRollNumber = await _dbSet
                .Where(se => se.ClassId == classId && se.AcademicYear == currentYear)
                .OrderByDescending(se => se.RollNumber)
                .Select(se => se.RollNumber)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (!string.IsNullOrEmpty(lastRollNumber))
            {
                var parts = lastRollNumber.Split('-');
                if (parts.Length > 2 && int.TryParse(parts[2], out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{className}-{currentYear}-{nextNumber:D3}";
        }
    }
}
