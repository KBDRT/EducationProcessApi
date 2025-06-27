using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface IDirectionService
    {
        public Task<(AppOperationStatus, Guid)> CreateAsync(string fullName, string shortName, string description);

        public Task<List<ArtDirection>> GetAsync();


    }
}
