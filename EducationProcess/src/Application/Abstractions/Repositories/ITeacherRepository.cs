using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Application.DTO;


namespace EducationProcessAPI.Application.Abstractions.Repositories
{
    public interface ITeacherRepository
    {
        public Task<Guid> CreateAsync(Teacher newTeacher);

        public Task<int> DeleteAllAsync();

        public Task<Teacher?> GetByIdAsync(Guid id);

        public Task<List<Teacher>> GetAfterWithSizeAsync(GetAfterIdWithPaginationDto request);

        public Task<Teacher?> UpdateAsync(Teacher teacher);

        public Task<List<Teacher>?> GetByEduYearAsync(int year);

    }
}
