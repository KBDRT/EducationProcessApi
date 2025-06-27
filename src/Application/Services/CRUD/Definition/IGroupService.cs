using CSharpFunctionalExtensions;
using EducationProcessAPI.Application.ServiceUtils;
using Microsoft.AspNetCore.Http;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface IGroupService
    {
        public Task<Result<Guid>> CreateAsync(string name, int startYear, Guid unionId);

        public Task CreateFromFileAsync(Guid unionId, IFormFile file, int year);
    }
}
