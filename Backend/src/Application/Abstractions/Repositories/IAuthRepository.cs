using EducationProcessAPI.Domain.Entities;

namespace Application.Abstractions.Repositories
{
    public interface IAuthRepository
    {
        public Task<Guid> CreateUserAsync(User newUser, CancellationToken cancellationToken = default);

        public Task<User?> GetUserByNameAsync(string userName, CancellationToken cancellationToken = default);
    }
}
