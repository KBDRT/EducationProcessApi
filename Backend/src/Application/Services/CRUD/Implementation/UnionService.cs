using Application;
using Application.Validators.Base;
using Application.Validators.CRUD.General;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class UnionService : IUnionService
    {
        private readonly IUnionRepository _unionRepository;
        private readonly IDirectionRepository _directionRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public UnionService(IUnionRepository unionRepository, 
                            IDirectionRepository directionRepository,
                            ITeacherRepository teacherRepository,
                            IValidatorFactoryCustom validatorFactory)
        {
            _unionRepository = unionRepository;
            _directionRepository = directionRepository;
            _teacherRepository = teacherRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<ServiceResultManager<Guid>> CreateAsync(CreateUnionDto artUnion)
        {
            var validation = _validatorFactory.GetValidator<CreateUnionDto>().Validate(artUnion);
            var serviceResult = new ServiceResultManager<Guid>(validation);

            if (!validation.IsValid)
            {
                return serviceResult;
            }
            
            Teacher? teacher = await _teacherRepository.GetByIdAsync(artUnion.TeacherId);
            if (teacher == null)
            {
                serviceResult.AddMessage("Teacher не найден", "TeacherId");
            }

            ArtDirection? direction = await _directionRepository.GetByIdAsync(artUnion.DirectionId);
            if (direction == null)
            {
                serviceResult.AddMessage("Direction не найден", "DirectionId");
            }

            if (teacher != null && direction != null)
            {
                ArtUnion newArtUnion = CreateNewUnion(artUnion, direction, teacher);
                var id = await _unionRepository.CreateAsync(newArtUnion);
                serviceResult.SetResultData(id);
            }

            return serviceResult;
        }

        private ArtUnion CreateNewUnion(CreateUnionDto artUnion, ArtDirection direction, Teacher teacher)
        {
            return new ArtUnion()
            {
                Id = Guid.NewGuid(),
                Description = artUnion.Description,
                Direction = direction,
                Teacher = teacher,
                Name = artUnion.Name,
                EduDuration = artUnion.Duration,
            };
        }

        public async Task<ServiceResultManager<ArtUnion?>> GetByIdAsync(Guid id)
        {
            var validation = new GuidEmptyValidator().Validate(id);
            var resultService = new ServiceResultManager<ArtUnion?>(validation);
            
            if (validation.IsValid)
            {
                var artUnion = await _unionRepository.GetByIdAsync(id);
                resultService.SetResultData(artUnion);
            }

            return resultService;
        }

        public async Task<ServiceResultManager<List<GetUnionDto>>> GetByTeacherIdAsync(Guid teacherId)
        {
            var validation = new GuidEmptyValidator().Validate(teacherId);
            var resultService = new ServiceResultManager<List<GetUnionDto>>(validation);

            if (validation.IsValid)
            {
                List<ArtUnion> unions = await _unionRepository.GetByTeacherIdAsync(teacherId);
                List<GetUnionDto> unionsResponse = CreateUnionResponse(unions);
                resultService.SetResultData(unionsResponse);
            }

            return resultService;
        }

        private List<GetUnionDto> CreateUnionResponse(List<ArtUnion> unions)
        {
            List<GetUnionDto> unionsResponse = new List<GetUnionDto>();

            foreach (var union in unions)
            {
                GetUnionDto unionResponse = new GetUnionDto()
                {
                    Id = union.Id,
                    Name = union.Name,
                    DirectionName = union.Direction.ShortName,
                    EduDuration = union.EduDuration,
                    Description = union.Description,
                    Groups = GetUnionDto.ListCreateGroup(union.Groups)
                };

                unionsResponse.Add(unionResponse);
            }

            return unionsResponse;
        }

    }
}
