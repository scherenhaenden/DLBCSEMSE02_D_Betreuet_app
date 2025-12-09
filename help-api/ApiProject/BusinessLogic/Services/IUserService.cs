using ApiProject.BusinessLogic.Models;

namespace ApiProject.BusinessLogic.Services
{
    public interface IUserService
    {
        Task<PaginatedResult<User>> GetAllAsync(int page, int pageSize, string? email = null, string? firstName = null, string? lastName = null, string? role = null);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateUserAsync(string firstName, string lastName, string email, string password, IEnumerable<string> roleNames);
        Task<bool> VerifyPasswordAsync(string email, string password);
        Task<bool> UserHasRoleAsync(Guid userId, string roleName);
    }
}
