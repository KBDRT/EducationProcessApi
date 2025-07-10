using Application.CQRS.Result.CQResult;
using Application.DTO;
using Application.Validators.Base;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities;
using MediatR;

namespace Application.CQRS.Teachers.Queries.GetTeachersPaginationAfter
{
    public class GetTeachersAfterIdQueryHandler : IRequestHandler<GetTeachersAfterIdQuery, CQResult<List<TeacherDto>>>
    {
        private readonly ITeacherRepository _teacherRepository;

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
                TeacherDto teacherDto = CreateTeacherResponse(teacher);
                teachersShortFormat.Add(teacherDto);
            }

            serviceResult.SetResultData(teachersShortFormat);

            return serviceResult;
        }

        private TeacherDto CreateTeacherResponse(Teacher teacher)
        {
            return new
            (
                teacher.Id,
                teacher.Initials.Surname,
                teacher.Initials.Name,
                teacher.Initials.Patronymic,
                teacher.BirthDate
            );
        }
    }
}
