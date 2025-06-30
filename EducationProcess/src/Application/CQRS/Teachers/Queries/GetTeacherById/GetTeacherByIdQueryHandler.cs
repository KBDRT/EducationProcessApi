using Application.CQRS.Helpers.CQResult;
using Application.DTO;
using Application.Validators.Base;
using Application.Validators.CRUD.General;
using DocumentFormat.OpenXml.Office2010.Excel;
using EducationProcessAPI.Application.Abstractions.Repositories;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Teachers.Queries.GetTeacherById
{
    public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, CQResult<TeacherDto>>
    {
        public readonly ITeacherRepository _teacherRepository;
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
                    TeacherDto teacherDto = new
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
    }
}
