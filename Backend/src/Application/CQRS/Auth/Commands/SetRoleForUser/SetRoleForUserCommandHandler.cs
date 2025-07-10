using Application.Abstractions.Repositories;
using Application.CQRS.Result.CQResult;
using Application.Helpers;
using Application.Validators.Base;
using AutoMapper;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.CQRS.Auth.Commands.SetRoleForUser
{
    public class SetRoleForUserCommandHandler : IRequestHandler<SetRoleForUserCommand, CQResult>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public SetRoleForUserCommandHandler(IAuthRepository authRepository,
                                            IValidatorFactoryCustom validatorFactory)
        {
            _authRepository = authRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<CQResult> Handle(SetRoleForUserCommand request, CancellationToken cancellationToken)
        {
            var result = new CQResult();
            await _authRepository.SetRoleForUserAsync(request.userId, request.roleId);

            return result;
        }

    }
}
