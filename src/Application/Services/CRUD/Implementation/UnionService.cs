using Application;
using CSharpFunctionalExtensions;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.Services.Helpers.Definition;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class UnionService : IUnionService
    {
        private readonly IUnionRepository _unionRepository;
        private readonly IDirectionRepository _directionRepository;
        private readonly ITeacherRepository _teacherRepository;

        private readonly IOperationResultService _operationResult;

        public UnionService(IUnionRepository unionRepository, 
                            IOperationResultService operationResult,
                            IDirectionRepository directionRepository,
                            ITeacherRepository teacherRepository)
        {
            _unionRepository = unionRepository;
            _operationResult = operationResult;
            _directionRepository = directionRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<Result<Guid>> CreateAsync(CreateUnionDto artUnion)
        {
            Teacher? teacher = await _teacherRepository.GetByIdAsync(artUnion.TeacherId);
            ArtDirection? direction = await _directionRepository.GetByIdAsync(artUnion.DirectionId);

            if (teacher == null || direction == null)
            {
                return Result.Failure<Guid>("Teacher or direction not found");
            }
            else
            {
                ArtUnion newArtUnion = new ArtUnion()
                {
                    Id = Guid.NewGuid(),
                    Description = artUnion.Description,
                    Direction = direction,
                    Teacher = teacher,
                    Name = artUnion.Name,
                    EduDuration = artUnion.Duration,
                };

                Guid id = await _unionRepository.CreateAsync(newArtUnion);

                return id.CheckGuidForEmpty();
            }
        }

        public async Task<ArtUnion?> GetByIdAsync(Guid id)
        {
            return await _unionRepository.GetByIdAsync(id);
        }

        public async Task<List<GetUnionDto>> GetByTeacherIdAsync(Guid teacherId)
        {
            List<ArtUnion> unions = await _unionRepository.GetByTeacherIdAsync(teacherId);

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
