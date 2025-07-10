using FluentValidation;

namespace Application.CQRS.Auth.Commands.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();

        }

    }
}
