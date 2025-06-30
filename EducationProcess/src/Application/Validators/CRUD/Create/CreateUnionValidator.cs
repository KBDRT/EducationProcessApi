using EducationProcessAPI.Application.DTO;
using FluentValidation;

namespace Application.Validators.CRUD.Create
{
    public class CreateUnionValidator : AbstractValidator<CreateUnionDto>
    {
        public CreateUnionValidator()
        {
            RuleFor(x => x.TeacherId).NotEqual(Guid.Empty);
            RuleFor(x => x.DirectionId).NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Duration).GreaterThan(0);
        }

    }
}
