using Application;
using Application.Cache.Definition;
using Application.Cache.Implementation;
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
        private const CacheManagerTypes _CACHE_TYPE = CacheManagerTypes.DistibutedRedis;

        private readonly IGroupRepository _groupRepository;
        private readonly IUnionRepository _unionRepository;
        private readonly IParseFile<Group> _fileParser;
        private readonly IValidatorFactoryCustom _validatorFactory;
        private readonly ICacheManager _cacheManager;

        public GroupService(IGroupRepository groupRepository, 
                            IUnionRepository unionRepository,
                            IParseFile<Group> fileParser,
                            IValidatorFactoryCustom validatorFactory,
                            ICacheManagerFactory cacheFactory)
        {
            _groupRepository = groupRepository;
            _unionRepository = unionRepository;
            _fileParser = fileParser;
            _validatorFactory = validatorFactory;
            _cacheManager = cacheFactory.Create(_CACHE_TYPE);
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
                    Group newGroup = CreateNewGroup(groupDto, union);
                    var id = await _groupRepository.CreateAsync(newGroup);

                    _cacheManager.Remove(GetCacheKey(newGroup.StartYear));
                    serviceResult.SetResultData(id);
                }
            }

            return serviceResult;
        }


        private Group CreateNewGroup(CreateGroupDto groupDto, ArtUnion union)
        {
            return new Group()
            {
                ArtUnion = union,
                Id = Guid.NewGuid(),
                Name = groupDto.GroupName,
                StartYear = groupDto.StartYear,
            };
        }

        private string GetCacheKey(int year) => $"{CackePrefixKeyConstants.TEACHERS_FOR_YEAR}_{year}";

        public async Task<ServiceResultManager> CreateFromFileAsync(CreateGroupFromFileDto groupDto)
        {
            var validation = _validatorFactory.GetValidator<CreateGroupFromFileDto>().Validate(groupDto);
            var serviceResult = new ServiceResultManager(validation);
            int startYear = groupDto.StartYear;

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
                ChangeGroupsList(ref groups, union, startYear);
                await _groupRepository.CreateRangeAsync(groups);
                _cacheManager.Remove(GetCacheKey(startYear));
            }

            return serviceResult;
        }

        private void ChangeGroupsList(ref List<Group> groups, ArtUnion union, int startYear)
        {
            foreach (var group in groups)
            {
                group.ArtUnion = union;
                group.Id = Guid.NewGuid();
                group.StartYear = startYear;

                group.Lessons = group.Lessons.Select(x =>
                {

                    if (x.Date.HasValue)
                    {
                        x.Date = new DateTime(startYear + x.Date.Value.Year - 1, x.Date.Value.Month, x.Date.Value.Day);
                    }
                    return x;
                }).ToList();

            }
        }

    }
}
