using EducationProcessAPI.Application.DTO;
using FluentValidation;

namespace Application.Validators.CRUD.Update
{
    public class UpdateTeacherValidator : AbstractValidator<TeacherDto>
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
