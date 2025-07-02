using Application;
using Application.DTO;
using Application.Validators.Base;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnionRepository _unionRepository;
        private readonly IParseFile<Group> _fileParser;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public GroupService(IGroupRepository groupRepository, 
                            IUnionRepository unionRepository,
                            IParseFile<Group> fileParser,
                            IValidatorFactoryCustom validatorFactory)
        {
            _groupRepository = groupRepository;
            _unionRepository = unionRepository;
            _fileParser = fileParser;
            _validatorFactory = validatorFactory;
        }

        public async Task<ServiceResultManager<Guid>> CreateAsync(CreateGroupDto groupDto)
        {
            var validation = _validatorFactory.GetValidator<CreateGroupDto>().Validate(groupDto);
            var serviceResult = new ServiceResultManager<Guid>(validation);

            if (validation.IsValid)
            {
                ArtUnion? union = await _unionRepository.GetByIdAsync(groupDto.UnionId);

                if (union == null)
                {
                    serviceResult.AddMessage("Union не найден", "unionId");
                }
                else
                {
                    Group newGroup = new Group()
                    {
                        ArtUnion = union,
                        Id = Guid.NewGuid(),
                        Name = groupDto.GroupName,
                        StartYear = groupDto.StartYear,
                    };

                    var id = await _groupRepository.CreateAsync(newGroup);

                    serviceResult.SetResultData(id);
                }
            }

            return serviceResult;
        }

        public async Task<ServiceResultManager> CreateFromFileAsync(CreateGroupFromFileDto groupDto)
        {
            var validation = _validatorFactory.GetValidator<CreateGroupFromFileDto>().Validate(groupDto);
            var serviceResult = new ServiceResultManager(validation);

            if (!validation.IsValid)
            {
                return serviceResult;
            }

            ArtUnion? union = await _unionRepository.GetByIdAsync(groupDto.UnionId);

            if (union == null)
            {
                serviceResult.AddMessage("Union не найден", "UnionId");
            }
            else
            {
                using var fileStream = groupDto.File.OpenReadStream();

                var groups = await _fileParser.ParseAsync(fileStream);

                foreach (var group in groups)
                {
                    group.ArtUnion = union;
                    group.Id = Guid.NewGuid();
                    group.StartYear = groupDto.StartYear;

                    group.Lessons = group.Lessons.Select(x =>
                    {

                        if (x.Date.HasValue)
                        {
                            x.Date = new DateTime(groupDto.StartYear + x.Date.Value.Year - 1, x.Date.Value.Month, x.Date.Value.Day);
                        }
                        return x;
                    }).ToList();

                }

                await _groupRepository.CreateRangeAsync(groups);
            }

            return serviceResult;
        }

    }
}
