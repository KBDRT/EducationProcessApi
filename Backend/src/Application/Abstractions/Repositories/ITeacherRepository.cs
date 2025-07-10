using Application.CQRS.Teachers.Queries.GetTeachersPaginationAfter;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities;


namespace EducationProcessAPI.Application.Abstractions.Repositories
{
    public interface ITeacherRepository
    {
        public Task<Guid> CreateAsync(Teacher newTeacher);

        public Task<int> DeleteAllAsync();

        public Task DeleteByIdAsync(Guid id);

        public Task<Teacher?> GetByIdAsync(Guid id);

        public Task<List<Teacher>> GetAfterWithSizeAsync(GetTeachersAfterIdQuery request);

        public Task<Teacher?> UpdateAsync(Teacher teacher);

        public Task<List<Teacher>?> GetByEduYearAsync(int year);

        public Task SetUserForTeacherAsync(Guid teacherId, Guid userId);
       

    }
}
