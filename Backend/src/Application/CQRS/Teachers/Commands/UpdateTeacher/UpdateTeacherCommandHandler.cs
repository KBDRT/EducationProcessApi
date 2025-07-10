using Application.CQRS.Result.CQResult;
using Application.Validators.Base;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities;
using MediatR;

namespace Application.CQRS.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, CQResult>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public UpdateTeacherCommandHandler(ITeacherRepository teacherRepository, 
                                           IValidatorFactoryCustom validatorFactory)
        {
            _teacherRepository = teacherRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<CQResult> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var validation = _validatorFactory.GetValidator<UpdateTeacherCommand>().Validate(request);
            var serviceResult = new CQResult(validation);

            if (validation.IsValid)
            {
                Teacher? teacherForUpdate = await _teacherRepository.GetByIdAsync(request.Id);
                if (teacherForUpdate == null)
                {
                    serviceResult.AddMessage("Teacher не найден", "TeacherId");
                }
                else
                {
                    teacherForUpdate.Initials.Surname = request.Surname;
                    teacherForUpdate.Initials.Patronymic = request.Patronymic;
                    teacherForUpdate.Initials.Name = request.Name;
                    teacherForUpdate.BirthDate = request.BirthDate;

                    var updatedTeacher = await _teacherRepository.UpdateAsync(teacherForUpdate);
                }
            }
            return serviceResult;
        }
    }
}
