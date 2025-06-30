using Application.DTO;
using FluentValidation;

namespace Application.Validators.CRUD.Create
{
    public class CreateDirectionValidator : AbstractValidator<CreateDirectionDto>
    {
        public CreateDirectionValidator()
        {
            RuleFor(x => x.ShortName).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }

    }
}
