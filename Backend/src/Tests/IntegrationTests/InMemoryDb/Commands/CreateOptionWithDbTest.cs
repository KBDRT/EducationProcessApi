using Application.Abstractions.Repositories;
using Application.CQRS.Analysis.Commands.CreateOption;
using Application.CQRS.Auth.Commands.RegisterUser;
using Application.CQRS.Result.CQResult;
using Application.Validators.Base;
using AutoFixture;
using AutoMapper;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation;
using Moq;

namespace Tests.IntegrationTests.Database.Commands
{
    public class CreateOptionWithDbTest
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<IValidatorFactoryCustom> _validatorFactoryMock = new();

        public CreateOptionWithDbTest()
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async void Should_Error_When_NoCriteriaForOptionInDb()
        {
            // Arrange
            using var db = new ApplicationContext(TestDBHelper.GetContextOptions());
            var repoReal = new AnalysisRepository(db);

            _validatorFactoryMock.Setup(x => x.GetValidator<CreateOptionCommand>())
                .Returns(new CreateOptionValidator());

            var command = _fixture.Build<CreateOptionCommand>().Create();
            var handler = new CreateOptionCommandHandler(repoReal, _validatorFactoryMock.Object);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.NotEqual(CQResultStatusCode.Success, result.ResultCode);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Guid.Empty, result.ResultData);
        }

        [Fact]
        public async void Should_Success_When_DbHasCriteriaForOption()
        {
            // Arrange
            using var db = new ApplicationContext(TestDBHelper.GetContextOptions());
            var repoReal = new AnalysisRepository(db);

            var criteriaId = Guid.NewGuid();
            var criteria = _fixture.Build<AnalysisCriteria>()
                .With(x => x.Id, criteriaId)
                .Create();
            await repoReal.CreateCriteriaAsync(criteria);

            _validatorFactoryMock.Setup(x => x.GetValidator<CreateOptionCommand>())
                .Returns(new CreateOptionValidator());

            var command = _fixture.Build<CreateOptionCommand>()
                .With(x => x.CriteriaId, criteriaId)
                .Create();
            var handler = new CreateOptionCommandHandler(repoReal, _validatorFactoryMock.Object);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.Equal(CQResultStatusCode.Success, result.ResultCode);
            Assert.Empty(result.Messages);
            Assert.NotEqual(Guid.Empty, result.ResultData);
        }

        [Fact]
        public async void Should_Error_When_EmptyOptionName()
        {
            // Arrange
            using var db = new ApplicationContext(TestDBHelper.GetContextOptions());
            var repoReal = new AnalysisRepository(db);

            _validatorFactoryMock.Setup(x => x.GetValidator<CreateOptionCommand>())
                .Returns(new CreateOptionValidator());

            var command = _fixture.Build<CreateOptionCommand>()
                .With(x => x.OptionName, string.Empty)
                .Create();
            var handler = new CreateOptionCommandHandler(repoReal, _validatorFactoryMock.Object);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.NotEqual(CQResultStatusCode.Success, result.ResultCode);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Guid.Empty, result.ResultData);
        }


        [Fact]
        public async void Should_ErrorAndZeroDbExec_When_EmptyCriteriaId()
        {
            // Arrange
            using var db = new ApplicationContext(TestDBHelper.GetContextOptions());
            var repoReal = new AnalysisRepository(db);

            var repoMock = new Mock<IAnalysisRepository>();
            repoMock.Setup(x => x.GetCriteriaByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                    .Returns((Guid id, CancellationToken ct) => repoReal.GetCriteriaByIdAsync(id, ct));
            repoMock.Setup(x => x.CreateOptionAsync(It.IsAny<CriterionOption>(), It.IsAny<CancellationToken>()))
                                   .Returns((CriterionOption opt, CancellationToken ct) => repoReal.CreateOptionAsync(opt, ct));

            _validatorFactoryMock.Setup(x => x.GetValidator<CreateOptionCommand>())
                .Returns(new CreateOptionValidator());

            var command = _fixture.Build<CreateOptionCommand>()
                .With(x => x.CriteriaId, Guid.Empty)
                .Create();
            var handler = new CreateOptionCommandHandler(repoReal, _validatorFactoryMock.Object);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.NotEqual(CQResultStatusCode.Success, result.ResultCode);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Guid.Empty, result.ResultData);

            repoMock.Verify(x => x.GetCriteriaByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
            repoMock.Verify(x => x.CreateOptionAsync(It.IsAny<CriterionOption>(), It.IsAny<CancellationToken>()), Times.Never);
        }



    }
}
