using EducationProcessAPI.Application.DTO;
using FluentValidation;

namespace Application.CQRS.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherValidator : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherValidator()
        {
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Patronymic).NotEmpty();
            RuleFor(x => x.BirthDate).InclusiveBetween(DateOnly.MinValue.AddYears(1000), DateOnly.MaxValue);
        }

    }
}
