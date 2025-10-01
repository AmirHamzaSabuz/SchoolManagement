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
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        public ClassRepository(SchoolDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Class>> GetClassesWithTeacherAsync()
        {
            return await _dbSet
                .Include(c => c.ClassTeacher)
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<Class?> GetClassWithSubjectsAsync(int classId)
        {
            return await _dbSet
                .Include(c => c.Subjects)
                .Include(c => c.ClassTeacher)
                .FirstOrDefaultAsync(c => c.Id == classId && c.IsActive);
        }

        public async Task<IReadOnlyList<Class>> GetAvailableClassesAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive && c.CurrentStrength < c.Capacity)
                .ToListAsync();
        }
    }

}
