using Application.Auth.Policy.Requirements;
using Application.CQRS.Auth.Queries.GetUserById;
using Application.CQRS.Result.CQResult;
using Domain.Entities.Auth;
using MediatR;
using Moq;

namespace Tests.UnitTests.AuthPolicies
{
    public class RoleRequiremenethHandlerTest
    {
        private RoleRequirementTestHelper _helper = new();


        private void SetRoleForUser(string roleName)
        {
            var commandGetUserResult = new CQResult<User>(); 
            var roles = new List<Role>() { new Role() { Name = roleName } };
            commandGetUserResult.SetResultData(new User() { Roles = roles });
            _helper.userMock
                .Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>())).
                ReturnsAsync(commandGetUserResult);
        }

        [Fact]
        public async void Should_SuccessAlways_When_AdminUser()
        {
            // Arrange
            var roleRequirement = _helper.requirement = new RoleRequirement("Head");
            _helper.RecreateContext([roleRequirement]);
            SetRoleForUser("Admin");
      
            // Act
            await _helper.StartHandler();

            // Assert
            Assert.True(_helper.context.HasSucceeded);
            Assert.False(_helper.context.HasFailed);
        }


        [Fact]
        public async void Should_Error_When_UserDoesntHaveRole()
        {
            // Arrange
            var roleRequirement = _helper.requirement = new RoleRequirement("Head");
            _helper.RecreateContext([roleRequirement]);
            SetRoleForUser("Teacher");

            // Act
            await _helper.StartHandler();

            // Assert
            Assert.True(_helper.context.HasFailed);
        }

        [Fact]
        public async void Should_Error_When_UserDoesntHaveAnyRole()
        {
            // Arrange
            var roleRequirement = _helper.requirement = new RoleRequirement("Teacher");
            _helper.RecreateContext([roleRequirement]);

            // Act
            await _helper.StartHandler();

            // Assert
            Assert.True(_helper.context.HasFailed);
        }




    }
}
