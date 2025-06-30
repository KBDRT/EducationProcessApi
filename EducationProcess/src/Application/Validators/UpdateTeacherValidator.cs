using Application.CQRS.Teachers.Commands.UpdateTeacher;
using Application.DTO;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateTeacherValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Patronymic).NotEmpty();
            RuleFor(x => x.BirthDate).InclusiveBetween(DateOnly.MinValue.AddYears(1000), DateOnly.MaxValue);
        }

    }
}
