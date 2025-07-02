using Application.Validators.Base;
using Application.Validators.CRUD.General;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities;
using Application.CQRS.Result.CQResult;
using MediatR;

namespace Application.CQRS.Teachers.Commands.UpdateTeacherBirthDate
{
    public class UpdateTeacherBirthDateCommandHandler : IRequestHandler<UpdateTeacherBirthDateCommand, CQResult>
    {
        private readonly ITeacherRepository _teacherRepository;

        public UpdateTeacherBirthDateCommandHandler(ITeacherRepository teacherRepository, 
                                           IValidatorFactoryCustom validatorFactory)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<CQResult> Handle(UpdateTeacherBirthDateCommand request, CancellationToken cancellationToken)
        {
            var validation = new GuidEmptyValidator().Validate(request.TeacherId);
            var serviceResult = new CQResult(validation);

            if (validation.IsValid)
            {
                Teacher? teacherForUpdate = await _teacherRepository.GetByIdAsync(request.TeacherId);
                if (teacherForUpdate == null)
                {
                    serviceResult.AddMessage("Teacher не найден", "TeacherId");
                }
                else
                {
                    teacherForUpdate.BirthDate = request.NewbirthDate;
                    var updatedTeacher = await _teacherRepository.UpdateAsync(teacherForUpdate);
                }
            }

            return serviceResult;
        }
    }
}
