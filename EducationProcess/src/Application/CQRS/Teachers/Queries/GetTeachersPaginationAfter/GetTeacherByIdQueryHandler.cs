using Application.CQRS.Helpers.CQResult;
using Application.DTO;
using Application.Validators.Base;
using Application.Validators.CRUD.General;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Teachers.Queries.GetTeachersPaginationAfter
{
    public class GetTeachersAfterIdQueryHandler : IRequestHandler<GetTeachersAfterIdQuery, CQResult<List<TeacherDto>>>
    {
        public readonly ITeacherRepository _teacherRepository;

        public GetTeachersAfterIdQueryHandler(ITeacherRepository teacherRepository,
                                    IValidatorFactoryCustom validatorFactory)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<CQResult<List<TeacherDto>>> Handle(GetTeachersAfterIdQuery request, CancellationToken cancellationToken)
        {
            var serviceResult = new CQResult<List<TeacherDto>>();

            List<Teacher> teachers = await _teacherRepository.GetAfterWithSizeAsync(request);

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
    }
}
