using Application.Abstractions.Repositories;
using Application.CQRS.Result.CQResult;
using Application.Helpers;
using Application.Validators.Base;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.CQRS.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, CQResult<string>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;
        private readonly JwtTokenGenerator _tokenGenerator;

        public LoginUserCommandHandler(IAuthRepository authRepository,
                                       IValidatorFactoryCustom validatorFactory,
                                       JwtTokenGenerator generator)
        {
            _authRepository = authRepository;
            _validatorFactory = validatorFactory;
            _tokenGenerator = generator;
        }

        public async Task<CQResult<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = new CQResult<string>();

            var user = await _authRepository.GetUserByNameAsync(request.Login, cancellationToken);
            if (user != null)
            {
                var verifiedResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHashed, request.Password);
                if (verifiedResult == PasswordVerificationResult.Success)
                {
                    var claims = CreateClaimForUser(user);
                    var token = _tokenGenerator.GetNewJwtTokenString(claims);
                    result.SetResultData(token);
                }
                else
                {
                    result.AddMessage("Неправильный пароль!", "Password");
                }
            }
            else
            {
                result.AddMessage("Пользователь не найден!", "Login");
            }

            return result;
        }

        private List<Claim> CreateClaimForUser(User user)
        {
            return
            [
                new Claim("user", user.Id.ToString()),
            ];
        }

    }
}
