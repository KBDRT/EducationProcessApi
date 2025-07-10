using Application.Abstractions.Repositories;
using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationContext _context;

        public AuthRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateRoleAsync(Role newRole, CancellationToken cancellationToken = default)
        {
            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();
            return newRole.Id;
        }

        public async Task<Guid> CreateUserAsync(User newUser, CancellationToken cancellationToken = default)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.Id;
        }

        public async Task<User?> GetUserByIdAsync(Guid id, bool addUserRole, CancellationToken cancellationToken = default)
        {
            if (addUserRole)
                return await _context.Users
                                .Include(x => x.Roles)
                                .ThenInclude(y => y.Permissions)
                                .SingleOrDefaultAsync(i => i.Id == id);

            return await _context.Users.SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<User?> GetUserByNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(i => i.Login == userName) ?? null;
        }

        public async Task<List<User>> GetUsersWithRolesPaginationAsync(int page, int size)
        {
            return await _context.Users
                                .Skip((page - 1) * size)
                                .Take(size)
                                .Include(x => x.Roles)
                                .ToListAsync();
        }

        public async Task SetRoleForUserAsync(Guid userId, Guid roleId)
        {
            var userTask = _context.Users.Include(u => u.Roles)
                                         .FirstOrDefaultAsync(u => u.Id == userId);

            var roleTask = _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            await Task.WhenAll(userTask, roleTask);

            var user = await userTask;
            var role = await roleTask;

            if (user != null && role != null)
            {
                user.Roles.Add(role);
                await _context.SaveChangesAsync();
            }

        }
    }
}
