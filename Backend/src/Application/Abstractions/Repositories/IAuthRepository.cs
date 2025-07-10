using Domain.Entities.Auth;

namespace Application.Abstractions.Repositories
{
    public interface IAuthRepository
    {
        public Task<Guid> CreateUserAsync(User newUser, CancellationToken cancellationToken = default);

        public Task<User?> GetUserByNameAsync(string userName, CancellationToken cancellationToken = default);

        public Task<User?> GetUserByIdAsync(Guid id, bool addUserRole, CancellationToken cancellationToken = default);

        public Task<Guid> CreateRoleAsync(Role newRole, CancellationToken cancellationToken = default);

        public Task SetRoleForUserAsync(Guid userId, Guid roleId);

        public Task<List<User>> GetUsersWithRolesPaginationAsync(int page, int size);
    }
}
