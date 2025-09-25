using SchoolManagement.Core.Entities;
using SchoolManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IReadOnlyList<User>> GetUsersByRoleAsync(UserRole role);
        Task<bool> IsEmailExistsAsync(string email);
        Task<IReadOnlyList<User>> GetTeachersAsync();
        Task<IReadOnlyList<User>> GetStudentsAsync();
    }
}
