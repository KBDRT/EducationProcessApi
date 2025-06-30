using Application;
using Application.DTO;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface IDirectionService
    {
        public Task<ServiceResultManager<Guid>> CreateAsync(CreateDirectionDto directionDto);

        public Task<ServiceResultManager<List<ArtDirection>>> GetAsync();

        public Task<ServiceResultManager<ArtDirection?>> GetByIdAsync(Guid id);


    }
}
