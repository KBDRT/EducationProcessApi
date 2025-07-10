using Application;
using Application.Cache.Definition;
using Application.Cache.Implementation;
using Application.Cache.Implementation.Memory;
using Application.CQRS.Result.CQResult;
using Application.DTO;
using Application.Validators.Base;
using AutoFixture;
using AutoFixture.AutoMoq;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Services.CRUD.Implementation;
using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Serilog;


namespace Tests.UnitTests.Services.GroupServiceOperations
{
    public class CreateGroup : IDisposable
    {
        private IFixture _fixture;

        public CreateGroup()
        {
            RecreateFix();

        }

        public void Dispose()
        {
            RecreateFix();
        }

        private void RecreateFix()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            var cacheManagerMock = new Mock<ICacheManager>();
            var cacheFactoryMock = new Mock<ICacheManagerFactory>();
            cacheFactoryMock
                .Setup(x => x.Create(It.IsAny<CacheManagerTypes>()))
                .Returns(cacheManagerMock.Object);
            _fixture.Inject(cacheFactoryMock.Object);
        }


        [Fact]
        public async void Should_Error_When_NoUnion()
        {
            // Arrange
            var unionRepo = new Mock<IUnionRepository>();
            unionRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ArtUnion?) null);
            _fixture.Inject(unionRepo.Object);

            var service = _fixture.Create<GroupService>();
            var newGroup = _fixture.Build<CreateGroupDto>()
                .With(x => x.UnionId, Guid.Empty)
                .Create();

            var groupRepo = _fixture.Freeze<Mock<IGroupRepository>>();

            // Act
            var result = await service.CreateAsync(newGroup);

            // Assert
            Assert.NotEmpty(result.Messages);
            Assert.Equal(ServiceResultCode.Error, result.ResultCode);
            Assert.Equal(Guid.Empty, result.ResultData);

            unionRepo.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
            groupRepo.Verify(x => x.CreateAsync(It.IsAny<Group>()), Times.Never());
        }

        [Fact]
        public async void Should_Success_When_HasUnion()
        {
            // Arrange
            var unionRepo = new Mock<IUnionRepository>();
            unionRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ArtUnion());
            _fixture.Inject(unionRepo.Object);

            var newGroup = _fixture.Build<CreateGroupDto>()
                .With(x => x.UnionId, Guid.NewGuid())
                .Create();

            var groupRepo = _fixture.Freeze<Mock<IGroupRepository>>();

            var service = _fixture.Create<GroupService>();

            // Act
            var result = await service.CreateAsync(newGroup);

            // Assert
            Assert.Empty(result.Messages);
            Assert.Equal(ServiceResultCode.Success, result.ResultCode);

            unionRepo.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
            groupRepo.Verify(x => x.CreateAsync(It.IsAny<Group>()), Times.Once());
        }



    }
}
