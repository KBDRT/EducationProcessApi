using EducationProcessAPI.Domain.Entities;


namespace EducationProcessAPI.Application.Abstractions.Repositories
{
    public interface IDirectionRepository
    {
        public Task<Guid> CreateAsync(ArtDirection artDirection);

        public Task<List<ArtDirection>> GetAsync();

        public Task<ArtDirection?> GetByIdAsync(Guid id);

    }
}
