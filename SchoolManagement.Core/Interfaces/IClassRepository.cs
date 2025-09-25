using SchoolManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Interfaces
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<IReadOnlyList<Class>> GetClassesWithTeacherAsync();
        Task<Class?> GetClassWithSubjectsAsync(int classId);
        Task<IReadOnlyList<Class>> GetAvailableClassesAsync();
    }
}
