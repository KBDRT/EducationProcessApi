using Application;
using CSharpFunctionalExtensions;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.Services.Helpers.Definition;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IOperationResultService _operationResult;

        public TeacherService(ITeacherRepository teacherRepository, 
                                IOperationResultService operationResult,
                                IGroupRepository groupRepository)
        {
            _teacherRepository = teacherRepository;
            _operationResult = operationResult;
        }

        public async Task<Result<Guid>> CreateAsync(string surname, string name, string patronymic, DateOnly birthDate)
        {
            var newTeacher = new Teacher() 
            { 
                Id = Guid.NewGuid(),
                Surname = surname,
                Name = name,
                Patronymic = patronymic,
                BirthDate = birthDate
            };

            var id = await _teacherRepository.CreateAsync(newTeacher);

            return id.CheckGuidForEmpty();
        }

        public async Task<int> DeleteAllAsync()
        {
            return await _teacherRepository.DeleteAllAsync();
        }

        public async Task<TeacherDto?> GetByIdAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            if (teacher != null)
            {
                TeacherDto teacherDto = new TeacherDto
                (
                    teacher.Id,
                    teacher.Surname,
                    teacher.Name,
                    teacher.Patronymic,
                    teacher.BirthDate
                );

                return teacherDto;
            }
            else
            {
                return null;
            }


        }

        public async Task<List<TeacherDto>> GetAfterWithSizeAsync(Guid afterId, int size)
        {
            GetAfterIdWithPaginationDto request = new GetAfterIdWithPaginationDto(afterId, size);

            List<Teacher> teachers = await _teacherRepository.GetAfterWithSizeAsync(request);

            List <TeacherDto> teachersShortFormat = new List<TeacherDto> ();

            foreach (Teacher teacher in teachers)
            {
                TeacherDto teacherDto = new TeacherDto
                (
                    teacher.Id,
                    teacher.Surname,
                    teacher.Name,
                    teacher.Patronymic,
                    teacher.BirthDate
                );

                teachersShortFormat.Add(teacherDto);
            }

            return teachersShortFormat;
        }

        public async Task<AppOperationStatus> UpdateAsync(TeacherDto teacherDto)
        {
            Teacher? dbTeacher = await _teacherRepository.GetByIdAsync(teacherDto.Id);
            if (dbTeacher == null)
            {
                return AppOperationStatus.NotFound;
            }

            dbTeacher.Surname = teacherDto.Surname;
            dbTeacher.BirthDate = teacherDto.BirthDate;
            dbTeacher.Patronymic = teacherDto.Patronymic;
            dbTeacher.Name = teacherDto.Name;

            var updatedTeacher = await _teacherRepository.UpdateAsync(dbTeacher);

            return GetStatusByNull(updatedTeacher);
        }

        public async Task<AppOperationStatus> UpdateBirthDateAsync(Guid teacherId, DateOnly birthDate)
        {
            Teacher? dbTeacher = await _teacherRepository.GetByIdAsync(teacherId);
            if (dbTeacher == null)
            {
                return AppOperationStatus.NotFound;
            }

            dbTeacher.BirthDate = birthDate;

            var updatedTeacher = await _teacherRepository.UpdateAsync(dbTeacher);

            return GetStatusByNull(updatedTeacher);
        }

        private AppOperationStatus GetStatusByNull(Teacher? teacher)
        {
            if (teacher == null)
            {
                return AppOperationStatus.NotFound;
            }
            else
            {
                return AppOperationStatus.Success;
            }
        }


        public async Task<List<TeachersForEduYearDto>> GetByEduYearAsync(int year)
        {
            var teachers = await _teacherRepository.GetByEduYearAsync(year);

            List<TeachersForEduYearDto> outputTeachers = new List<TeachersForEduYearDto>();

            foreach (var teacher in teachers)
            {
                List<UnionNameDto> unionsShort = new List<UnionNameDto>();

                foreach (var union in teacher.Union)
                {
                    List<GroupsNameDto> groups = new List<GroupsNameDto>();

                    foreach (var group in union.Groups)
                    {
                        GroupsNameDto groupShort = new(group.Name, group.Id);
                        groups.Add(groupShort);
                    }

                    UnionNameDto unionShort = new(union.Name, groups);
                    unionsShort.Add(unionShort);
                }

                TeachersForEduYearDto teacherShort = new(teacher.Initials, unionsShort);
                outputTeachers.Add(teacherShort);
            }

            return outputTeachers;
        }

    }
}
