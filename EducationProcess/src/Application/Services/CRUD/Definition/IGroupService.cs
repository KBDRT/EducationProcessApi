using Application;
using Application.DTO;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface IGroupService
    {
        public Task<ServiceResultManager<Guid>> CreateAsync(CreateGroupDto groupDto);

        public Task<ServiceResultManager> CreateFromFileAsync(CreateGroupFromFileDto groupDto);
    }
}
