using Application.CQRS.Result.CQResult;
using Application.DTO;
using Application.Validators.Base;
using Application.Validators.CRUD.General;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities;
using MediatR;

namespace Application.CQRS.Teachers.Queries.GetTeacherById
{
    public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, CQResult<TeacherDto>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public GetTeacherByIdQueryHandler(ITeacherRepository teacherRepository,
                                    IValidatorFactoryCustom validatorFactory)
        {
            _teacherRepository = teacherRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<CQResult<TeacherDto>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            var validation = new GuidEmptyValidator().Validate(request.TeacherId);
            var serviceResult = new CQResult<TeacherDto>(validation);

            if (validation.IsValid)
            {
                var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId);
                if (teacher != null)
                {
                    TeacherDto teacherDto = CreateTeacherResponse(teacher);
                    serviceResult.SetResultData(teacherDto);
                }
            }

            return serviceResult;
        }


        private TeacherDto CreateTeacherResponse(Teacher teacher)
        {
            return new
            (
                teacher.Id,
                teacher.Surname,
                teacher.Name,
                teacher.Patronymic,
                teacher.BirthDate
            );
        }
    }
}
