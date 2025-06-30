using Application.DTO;
using FluentValidation;

namespace Application.Validators.CRUD.Create
{
    public class CreateGroupFromFileValidator : AbstractValidator<CreateGroupFromFileDto>
    {

        public CreateGroupFromFileValidator()
        {
            RuleFor(x => x.UnionId).NotEqual(Guid.Empty);
            RuleFor(x => x.StartYear).InclusiveBetween(2000, 9999);
            RuleFor(x => x.File.Length).NotEmpty();
        }

    }
}
