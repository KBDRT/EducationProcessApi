using FluentValidation;
using System.Text.RegularExpressions;

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

            RuleFor(x => x.Password).Must(CheckPassword).WithMessage("Пароль должен быть минимум 8 символов из букв и цифры"); ;


        }

        //  минимум 8 символов, буквы, цифры
        private readonly Regex _passwordRegex = new(@"^(\d|[A-Za-z]){8,}$");

        private bool CheckPassword(string password) => _passwordRegex.IsMatch(password);
    }
}
