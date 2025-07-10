using Application.Abstractions.Repositories;
using Application.CQRS.Result.CQResult;
using Application.Helpers;
using Application.Validators.Base;
using AutoMapper;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.CQRS.Auth.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CQResult<Guid>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IAuthRepository authRepository,
                                       IValidatorFactoryCustom validatorFactory,
                                       IMapper mapper)
        {
            _authRepository = authRepository;
            _validatorFactory = validatorFactory;
            _mapper = mapper;
        }

        public async Task<CQResult<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = new CQResult<Guid>();

            var newRole = _mapper.Map<Role>(request);
            var id = await _authRepository.CreateRoleAsync(newRole, cancellationToken);
            result.SetResultData(id);

            return result;
        }

    }
}
