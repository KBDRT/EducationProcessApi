using Application;
using Application.DTO;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities;
using Application.Validators.CRUD.General;
using Application.Validators.Base;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class DirectionService : IDirectionService
    {
        private readonly IDirectionRepository _directionRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public DirectionService(IDirectionRepository directionRepository,
                                IValidatorFactoryCustom validatorFactory)
        {
            _directionRepository = directionRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<ServiceResultManager<Guid>> CreateAsync(CreateDirectionDto directionDto)
        {
            var validation = _validatorFactory.GetValidator<CreateDirectionDto>().Validate(directionDto);
            var serviceResult = new ServiceResultManager<Guid>(validation);

            if (validation.IsValid)
            {
                var newDirection = new ArtDirection()
                {
                    Id = Guid.NewGuid(),
                    Description = directionDto.Description,
                    FullName = directionDto.FullName,
                    ShortName = directionDto.ShortName,
                };

                var id = await _directionRepository.CreateAsync(newDirection);
                serviceResult.SetResultData(id);
            }

            return serviceResult;
        }

        public async Task<ServiceResultManager<List<ArtDirection>>> GetAsync()
        {
            var serviceResult = new ServiceResultManager<List<ArtDirection>>();
            var artDirections = await _directionRepository.GetAsync();
            serviceResult.SetResultData(artDirections);

            return serviceResult;
        }

        public async Task<ServiceResultManager<ArtDirection?>> GetByIdAsync(Guid id)
        {
            var validation = new GuidEmptyValidator().Validate(id);
            var serviceResult = new ServiceResultManager<ArtDirection?>(validation);

            if (validation.IsValid)
            {
                var artDirection = await _directionRepository.GetByIdAsync(id);
                serviceResult.SetResultData(artDirection);
            }

            return serviceResult;
        }


    }
}
