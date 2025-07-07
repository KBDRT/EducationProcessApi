using Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities;
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

        public async Task<Guid> CreateUserAsync(User newUser, CancellationToken cancellationToken = default)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.Id;
        }

        public async Task<User?> GetUserByNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(i => i.Login == userName) ?? null;
        }
    }
}
