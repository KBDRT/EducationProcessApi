using Application;
using CSharpFunctionalExtensions;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnionRepository _unionRepository;
        private readonly IParseFile<Group> _fileParser;

        public GroupService(IGroupRepository groupRepository, 
                            IUnionRepository unionRepository,
                            IParseFile<Group> fileParser)
        {
            _groupRepository = groupRepository;
            _unionRepository = unionRepository;
            _fileParser = fileParser;
        }

        public async Task<Result<Guid>> CreateAsync(string name, int startYear, Guid unionId)
        {
            ArtUnion? union = await _unionRepository.GetByIdAsync(unionId);

            if (union == null)
            {
                return Result.Failure<Guid>("Union not found");
            }
            else
            {
                Group newGroup = new Group()
                {
                    ArtUnion = union,
                    Id = Guid.NewGuid(),
                    Name = name,
                    StartYear = startYear,
                };

                Guid id = await _groupRepository.CreateAsync(newGroup);

                return id.CheckGuidForEmpty();
            }
        }

        public async Task CreateFromFileAsync(Guid unionId, IFormFile file, int year)
        {
            ArtUnion? union = await _unionRepository.GetByIdAsync(unionId);

            if (union == null)
            {
                return;
            }

            using var fileStream = file.OpenReadStream();

            var groups = await _fileParser.ParseAsync(fileStream);

            foreach (var group in groups)
            {
                group.ArtUnion = union;
                group.Id = Guid.NewGuid();
                group.StartYear = year;

                group.Lessons = group.Lessons.Select(x =>
                {
                    
                    if (x.Date.HasValue)
                    {
                        x.Date = new DateTime(year + x.Date.Value.Year - 1, x.Date.Value.Month, x.Date.Value.Day);
                    }
                    return x;
                }).ToList();

            }

            await _groupRepository.CreateRangeAsync(groups);

        }

    }
}
