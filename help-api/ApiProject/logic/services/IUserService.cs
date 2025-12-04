using ApiProject.Db;
using ApiProject.Db.Entities;

namespace ApiProject.Logic.Services;

public interface IUserService
{
    IReadOnlyCollection<User> GetAll();
    User? GetById(Guid id);

    User CreateUser(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        IEnumerable<string> roleNames);

    bool UserHasRole(Guid userId, string roleName);
    Role EnsureRole(string roleName);
}