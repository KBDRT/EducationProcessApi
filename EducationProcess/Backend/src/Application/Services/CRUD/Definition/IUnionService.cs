using Application;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface IUnionService
    {
        public Task<ServiceResultManager<Guid>> CreateAsync(CreateUnionDto artUnion);

        public Task<ServiceResultManager<List<GetUnionDto>>> GetByTeacherIdAsync(Guid teacherId);

        public Task<ServiceResultManager<ArtUnion?>> GetByIdAsync(Guid id);
    }
}
