using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Abstractions.Repositories
{
    public interface IGroupRepository
    {
        public Task<Guid> CreateAsync(Group newGroup);


        public Task<Group?> GetByIdAsync(Guid id);

        public Task CreateRangeAsync(List<Group> groups);

  



    }
}
