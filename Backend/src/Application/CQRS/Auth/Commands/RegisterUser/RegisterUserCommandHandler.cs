
using Application.Abstractions.Repositories;
using Application.CQRS.Result.CQResult;
using Application.Validators.Base;
using EducationProcessAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, CQResult<Guid>>
    {

        private readonly IAuthRepository _authRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public RegisterUserCommandHandler(IAuthRepository authRepository,
                                               IValidatorFactoryCustom validatorFactory)
        {
            _authRepository = authRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<CQResult<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            User newUser = new()
            {
                Id = Guid.NewGuid(),
                Login = request.Login,
            };

            var hashedPassword = new PasswordHasher<User>().HashPassword(newUser, request.Password);
            newUser.Password = hashedPassword;

            var result = new CQResult<Guid>();
            
            var id = await _authRepository.CreateUserAsync(newUser);
            result.SetResultData(id);

            return result;
        }
    }
}
