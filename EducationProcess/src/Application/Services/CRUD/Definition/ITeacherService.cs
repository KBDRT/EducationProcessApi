using Application;
using CSharpFunctionalExtensions;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.ServiceUtils;
namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface ITeacherService
    {
        public Task<ServiceResultManager<Guid>> CreateAsync(CreateTeacherDto teacherDto);

        public Task<ServiceResultManager<TeacherDto?>> GetByIdAsync(Guid id);

        public Task<ServiceResultManager<List<TeacherDto>>> GetAfterWithSizeAsync(Guid afterId, int size);

        public Task<ServiceResultManager<int>> DeleteAllAsync();

        public Task<ServiceResultManager> UpdateAsync(TeacherDto teacherDto);

        public Task<ServiceResultManager> UpdateBirthDateAsync(Guid teacherId, DateOnly birthDate);

        public Task<ServiceResultManager<List<TeachersForEduYearDto>>> GetByEduYearAsync(int year);

    }
}
