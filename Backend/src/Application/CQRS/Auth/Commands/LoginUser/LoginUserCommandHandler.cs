using Application.Abstractions.Repositories;
using Application.CQRS.Result.CQResult;
using Application.Validators.Base;
using EducationProcessAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.CQRS.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, CQResult<string>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public LoginUserCommandHandler(IAuthRepository authRepository,
                                       IValidatorFactoryCustom validatorFactory)
        {
            _authRepository = authRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<CQResult<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _authRepository.GetUserByNameAsync(request.Login);

            var key = "KEYKEYKEYKEYKEYKEYKEYKEYKEYKEYKEYKEY";

            var result = new CQResult<string>();

            if (user != null)
            {
                var verifiedResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password);

                var jwtToken = new JwtSecurityToken(
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256
                    ));

                var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                result.SetResultData(jwtTokenString);
            }


            return result;
        }
    }
}
