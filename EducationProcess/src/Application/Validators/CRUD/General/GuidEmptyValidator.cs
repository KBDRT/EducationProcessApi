using FluentValidation;

namespace Application.Validators.CRUD.General
{
    public class GuidEmptyValidator : AbstractValidator<Guid>
    {

        public GuidEmptyValidator()
        {
            RuleFor(x => x).NotEqual(Guid.Empty);
        }

    }
}
