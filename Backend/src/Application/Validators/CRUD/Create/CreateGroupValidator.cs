using Application.DTO;
using FluentValidation;

namespace Application.Validators.CRUD.Create
{
    public class CreateGroupValidator : AbstractValidator<CreateGroupDto>
    {
        public CreateGroupValidator()
        {
            RuleFor(x => x.GroupName).NotEmpty();
            RuleFor(x => x.StartYear).NotEmpty();
            RuleFor(x => x.StartYear).InclusiveBetween(2000, 9999);
            RuleFor(x => x.UnionId).NotEqual(Guid.Empty);
        }

    }
}
