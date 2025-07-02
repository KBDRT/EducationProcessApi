using Application.CQRS.Result.CQResult;
using Application.Validators.Base;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Teachers.Commands.CreateTeacher
{
    public class DeleteAllTeachersCommandHandler : IRequestHandler<CreateTeacherCommand, CQResult<Guid>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public DeleteAllTeachersCommandHandler(ITeacherRepository teacherRepository,
                                    IValidatorFactoryCustom validatorFactory)
        {
            _teacherRepository = teacherRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<CQResult<Guid>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var validation = _validatorFactory.GetValidator<CreateTeacherCommand>().Validate(request);
            var serviceResult = new CQResult<Guid>(validation);

            if (validation.IsValid)
            {
                var newTeacher = new Teacher()
                {
                    Id = Guid.NewGuid(),
                    Surname = request.Surname,
                    Name = request.Name,
                    Patronymic = request.Patronymic,
                    BirthDate = request.BirthDate
                };

                var id = await _teacherRepository.CreateAsync(newTeacher);
                serviceResult.SetResultData(id);
            }

            return serviceResult;
        }
    }
}
