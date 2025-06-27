using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface ITeacherService
    {
        public Task<(AppOperationStatus, Guid)> CreateAsync(string surname, string name, string patronymic, DateOnly birthDate);

        public Task<TeacherDto?> GetByIdAsync(Guid id);

        public Task<List<TeacherDto>> GetAfterWithSizeAsync(Guid afterId, int size);

        public Task<int> DeleteAllAsync();

        public Task<AppOperationStatus> UpdateAsync(TeacherDto teacherDto);

        public Task<AppOperationStatus> UpdateBirthDateAsync(Guid teacherId, DateOnly birthDate);

        public Task<List<TeachersForEduYearDto>> GetByEduYearAsync(int year);

    }
}
