using Application;
using Application.Validators.Base;
using Application.Validators.CRUD.General;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Domain.Entities;
using FluentValidation;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public TeacherService(ITeacherRepository teacherRepository,
                                IValidatorFactoryCustom validatorFactory)
        {
            _teacherRepository = teacherRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<ServiceResultManager<Guid>> CreateAsync(CreateTeacherDto teacherDto)
        {
            var validation = _validatorFactory.GetValidator<CreateTeacherDto>().Validate(teacherDto);
            var serviceResult = new ServiceResultManager<Guid>(validation);

            if (validation.IsValid)
            {
                var newTeacher = new Teacher()
                {
                    Id = Guid.NewGuid(),
                    Surname = teacherDto.Surname,
                    Name = teacherDto.Name,
                    Patronymic = teacherDto.Patronymic,
                    BirthDate = teacherDto.BirthDate
                };

                var id = await _teacherRepository.CreateAsync(newTeacher);
                serviceResult.SetResultData(id);    
            }

            return serviceResult;

        }

        public async Task<ServiceResultManager<int>> DeleteAllAsync()
        {
            var serviceResult = new ServiceResultManager<int>();
            var deletedCount = await _teacherRepository.DeleteAllAsync();

            serviceResult.SetResultData(deletedCount);

            return serviceResult;
        }

        public async Task<ServiceResultManager<TeacherDto?>> GetByIdAsync(Guid id)
        {
            var validation = new GuidEmptyValidator().Validate(id);
            var serviceResult = new ServiceResultManager<TeacherDto?>(validation);

            if (validation.IsValid)
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

                    serviceResult.SetResultData(teacherDto);
                }
            }

            return serviceResult;
        }

        public async Task<ServiceResultManager<List<TeacherDto>>> GetAfterWithSizeAsync(Guid afterId, int size)
        {
            var serviceResult = new ServiceResultManager<List<TeacherDto>>();

            List<Teacher> teachers = await _teacherRepository.GetAfterWithSizeAsync(new GetAfterIdWithPaginationDto(afterId, size));

            List<TeacherDto> teachersShortFormat = [];
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
            serviceResult.SetResultData(teachersShortFormat);

            return serviceResult;
        }

        public async Task<ServiceResultManager> UpdateAsync(TeacherDto teacherDto)
        {
            var validation = _validatorFactory.GetValidator<TeacherDto>().Validate(teacherDto);
            var serviceResult = new ServiceResultManager(validation);

            if (validation.IsValid)
            {
                Teacher? teacherForUpdate = await _teacherRepository.GetByIdAsync(teacherDto.Id);
                if (teacherForUpdate == null)
                {
                    serviceResult.AddMessage("Teacher не найден", "TeacherId");
                }
                else
                {
                    teacherForUpdate.Surname = teacherDto.Surname;
                    teacherForUpdate.BirthDate = teacherDto.BirthDate;
                    teacherForUpdate.Patronymic = teacherDto.Patronymic;
                    teacherForUpdate.Name = teacherDto.Name;

                    var updatedTeacher = await _teacherRepository.UpdateAsync(teacherForUpdate);
                }
            }
            return serviceResult;
        }

        public async Task<ServiceResultManager> UpdateBirthDateAsync(Guid teacherId, DateOnly birthDate)
        {
            var validation = new GuidEmptyValidator().Validate(teacherId);
            var serviceResult = new ServiceResultManager(validation);

            if (validation.IsValid)
            {
                Teacher? teacherForUpdate = await _teacherRepository.GetByIdAsync(teacherId);
                if (teacherForUpdate == null)
                {
                    serviceResult.AddMessage("Teacher не найден", "TeacherId");
                }
                else
                {
                    teacherForUpdate.BirthDate = birthDate;
                    var updatedTeacher = await _teacherRepository.UpdateAsync(teacherForUpdate);
                }
            }

            return serviceResult;
        }


        public async Task<ServiceResultManager<List<TeachersForEduYearDto>>> GetByEduYearAsync(int year)
        {
            var validator = new InlineValidator<int>();
            validator.RuleFor(x => x).InclusiveBetween(2000, 9999);
            var validation = validator.Validate(year);

            var serviceResult = new ServiceResultManager<List<TeachersForEduYearDto>>(validation);

            if (validation.IsValid)
            {
                var teachers = await _teacherRepository.GetByEduYearAsync(year);

                List<TeachersForEduYearDto> outputTeachers = new List<TeachersForEduYearDto>();

                if (teachers != null)
                {
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
                }
            }

            return serviceResult;
        }

    }
}
