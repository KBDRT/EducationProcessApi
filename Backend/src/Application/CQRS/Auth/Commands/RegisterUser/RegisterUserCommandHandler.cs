
using Application.Abstractions.Repositories;
using Application.CQRS.Analysis.Commands.CreateOption;
using Application.CQRS.Result.CQResult;
using Application.Mapping;
using Application.Validators.Base;
using AutoMapper;
using Domain.Entities.Auth;
using EducationProcessAPI.Application.Abstractions.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, CQResult<Guid>>
    {

        private readonly IAuthRepository _authRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IAuthRepository authRepository,
                                          IValidatorFactoryCustom validatorFactory,
                                          ITeacherRepository teacherRepository,
                                          IMapper mapper)
        {
            _authRepository = authRepository;
            _validatorFactory = validatorFactory;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<CQResult<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken = default)
        {
            var validation = _validatorFactory.GetValidator<RegisterUserCommand>().Validate(request);
            var result = new CQResult<Guid>(validation);

            if (!validation.IsValid)
            {
                return result;
            }

            var userInBase = await _authRepository.GetUserByNameAsync(request.Login, cancellationToken);
            if (userInBase != null)
            {
                result.AddMessage("Пользователь с таким логином уже зарегистрирован!", "Login");
                return result;
            }

            var newUser = _mapper.Map<User>(request);
            newUser.Id = Guid.NewGuid();

            var hashedPassword = new PasswordHasher<User>().HashPassword(newUser, request.Password);
            newUser.PasswordHashed = hashedPassword;

            var id = await _authRepository.CreateUserAsync(newUser);
            result.SetResultData(id);
            
            if (request.TeacherId != Guid.Empty)
            {
                await _teacherRepository.SetUserForTeacherAsync((Guid)request.TeacherId, id);
            }

            return result;
        }
    }
}
