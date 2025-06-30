using Application;
using EducationProcessAPI.Application.DTO;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface ILessonService
    {
        public Task<ServiceResultManager<Guid>> CreateAsync(LessonDto lesson);

        public Task<ServiceResultManager<List<LessonsDateDto>?>> GetByGroupIdAsync(Guid id);

        public Task<ServiceResultManager<LessonShortDto?>> GetByIdAsync(Guid id);
    }
}
