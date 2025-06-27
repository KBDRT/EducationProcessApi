using Application;
using CSharpFunctionalExtensions;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.Services.Helpers.Definition;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class DirectionService : IDirectionService
    {
        private readonly IDirectionRepository _directionRepository;
        private readonly IOperationResultService _operationResult;

        public DirectionService(IDirectionRepository directionRepository, IOperationResultService operationResult)
        {
            _directionRepository = directionRepository;
            _operationResult = operationResult;
        }

        public async Task<Result<Guid>> CreateAsync(string fullName, string shortName, string description)
        {
            var newDirection = new ArtDirection()
            {
                Id = Guid.NewGuid(),
                Description = description,
                FullName = fullName,
                ShortName = shortName,
            };

            var id = await _directionRepository.CreateAsync(newDirection);

            return id.CheckGuidForEmpty();
        }

        public async Task<List<ArtDirection>> GetAsync()
        {
            return await _directionRepository.GetAsync();   
        }

        public async Task<ArtDirection?> GetByIdAsync(Guid id)
        {
            return await _directionRepository.GetByIdAsync(id);
        }


    }
}
