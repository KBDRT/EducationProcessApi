using Application.Abstractions.Repositories;
using Application.CQRS.Auth.Commands.RegisterUser;
using Application.CQRS.Result.CQResult;
using Application.Mapping;
using Application.Validators.Base;
using AutoFixture;
using AutoMapper;
using Domain.Entities.Auth;
using EducationProcessAPI.Application.Abstractions.Repositories;
using Moq;

namespace Tests.Unit.Commands.Auth
{
    public class RegisterUserHandlerTest : IDisposable
    {
        private readonly IFixture _fixture = new Fixture();

        private readonly Mock<IAuthRepository> _authMock = new();
        private readonly Mock<ITeacherRepository> _teacherMock = new();
        private readonly Mock<IValidatorFactoryCustom> _validatorFactoryMock = new();
        private readonly IMapper _mapper;

        public RegisterUserHandlerTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfileDto>();
            });

            _mapper = config.CreateMapper();

            _validatorFactoryMock.Setup(x => x.GetValidator<RegisterUserCommand>())
                .Returns(new RegisterUserValidator());
        }


        public void Dispose()
        {
            _authMock.Reset();
            _teacherMock.Reset();
            //_validatorFactoryMock.Reset();
        }

        [Fact]
        public async Task Should_SuccessResult_When_ValidData()
        {
            // Arrange
            _authMock.Setup(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(Guid.NewGuid());

            var command = _fixture.Build<RegisterUserCommand>()
                                  .With(x => x.TeacherId, Guid.Empty)
                                  .Create();
            var handler = new RegisterUserCommandHandler(_authMock.Object,
                                                         _validatorFactoryMock.Object,
                                                         _teacherMock.Object,
                                                         _mapper);

            // Act
            var commandResult = await handler.Handle(command);

            // Assert
            Assert.Empty(commandResult.Messages);
            Assert.Equal(CQResultStatusCode.Success, commandResult.ResultCode);
            Assert.NotEqual(Guid.Empty, commandResult.ResultData);

            _authMock.Verify(x => x.CreateUserAsync(
                It.IsAny<User>(), 
                It.IsAny<CancellationToken>()),
                Times.Once);

        }


        [Fact]
        public async Task Should_NotSetUserForTeacher_When_TeacherIdEmpty()
        {
            // Arrange
            _authMock.Setup(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(Guid.NewGuid());
            _teacherMock.Setup(x => x.SetUserForTeacherAsync(It.IsAny<Guid>(), It.IsAny<Guid>()));

            var command = _fixture.Build<RegisterUserCommand>()
                                  .With(x => x.TeacherId, Guid.Empty)
                                  .Create();
            var handler = new RegisterUserCommandHandler(_authMock.Object,
                                                         _validatorFactoryMock.Object,
                                                         _teacherMock.Object,
                                                         _mapper);

            // Act
            var commandResult = await handler.Handle(command);

            // Assert
            Assert.Empty(commandResult.Messages);
            Assert.Equal(CQResultStatusCode.Success, commandResult.ResultCode);
            Assert.NotEqual(Guid.Empty, commandResult.ResultData);

            _authMock.Verify(x => x.CreateUserAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()),
                Times.Once);

            _teacherMock.Verify(x => x.SetUserForTeacherAsync(
              It.IsAny<Guid>(),
              It.IsAny<Guid>()),
              Times.Never);
        }

        [Fact]
        public async Task Should_ErrorResult_When_EmptyPassword()
        {
            // Arrange
            _authMock.Setup(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(Guid.NewGuid());

            _teacherMock.Setup(x => x.SetUserForTeacherAsync(It.IsAny<Guid>(), It.IsAny<Guid>()));

            var command = _fixture.Build<RegisterUserCommand>()
                               .With(x => x.TeacherId, Guid.Empty)
                               .With(x => x.Password, string.Empty)
                               .Create();

            var handler = new RegisterUserCommandHandler(_authMock.Object,
                                                         _validatorFactoryMock.Object,
                                                         _teacherMock.Object,
                                                         _mapper);

            // Act
            var commandResult = await handler.Handle(command);

            // Assert
            Assert.NotEmpty(commandResult.Messages);
            Assert.Equal(CQResultStatusCode.Error, commandResult.ResultCode);
            Assert.Equal(Guid.Empty, commandResult.ResultData);

            _authMock.Verify(x => x.CreateUserAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()),
                Times.Never);

            _teacherMock.Verify(x => x.SetUserForTeacherAsync(
                It.IsAny<Guid>(),
                It.IsAny<Guid>()),
                Times.Never);
        }
    }
}