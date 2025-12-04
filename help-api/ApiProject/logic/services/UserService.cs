using ApiProject.Db.Entities;
using ApiProject.Db.Context;

namespace ApiProject.Logic.Services;

public sealed class UserService : IUserService
{
    private readonly ThesisDbContext _context;

    public UserService(ThesisDbContext context)
    {
        _context = context;
    }

    public IReadOnlyCollection<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User? GetById(Guid id)
    {
        return _context.Users.SingleOrDefault(u => u.Id == id);
    }

    public Role EnsureRole(string roleName)
    {
        var normalized = roleName.Trim().ToUpperInvariant();

        var existing = _context.Roles.SingleOrDefault(r => r.Name == normalized);
        if (existing is not null)
        {
            return existing;
        }

        var role = new Role
        {
            Name = normalized
        };

        _context.Roles.Add(role);
        _context.SaveChanges();
        return role;
    }

    public bool UserHasRole(Guid userId, string roleName)
    {
        var user = GetById(userId);
        if (user is null)
        {
            return false;
        }

        var normalized = roleName.Trim().ToUpperInvariant();

        return user.UserRoles.Any(ur =>
            ur.Role is not null &&
            ur.Role.Name == normalized);
    }

    public User CreateUser(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        IEnumerable<string> roleNames)
    {
        if (_context.Users.Any(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("A user with this e-mail already exists.");
        }

        var user = new User
        {
            FirstName    = firstName.Trim(),
            LastName     = lastName.Trim(),
            Email        = email.Trim(),
            PasswordHash = passwordHash // assume already hashed
        };

        foreach (var roleName in roleNames)
        {
            var role = EnsureRole(roleName);

            var userRole = new UserRole
            {
                User   = user,
                UserId = user.Id,
                Role   = role,
                RoleId = role.Id
            };

            user.UserRoles.Add(userRole);
            role.UserRoles.Add(userRole);
        }

        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }
}
