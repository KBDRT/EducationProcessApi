using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Abstractions.Repositories
{
    public interface ILessonRepository
    {
        public Task<Guid> CreateAsync(Lesson newLesson);


        public Task<List<Lesson>?> GetByGroupIdAsync(Guid groupId);

        public Task<Lesson?> GetByIdAsync(Guid id);

        public Task<Lesson?> GetWithIncludesByIdAsync(Guid id);

    }
}
