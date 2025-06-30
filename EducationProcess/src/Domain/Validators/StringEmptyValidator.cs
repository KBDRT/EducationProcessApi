using FluentValidation;

namespace Domain.Validators
{
    public class StringEmptyValidator : AbstractValidator<string>
    {
        public StringEmptyValidator()
        {
            RuleFor(x => x).NotEmpty();
        }
    }
}
