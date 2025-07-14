using Application.Auth.Policy.Requirements;
using Application.CQRS.Auth.Queries.GetUserById;
using Application.CQRS.Result.CQResult;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Moq;
using System.Security.Claims;

namespace Tests.UnitTests.AuthPolicies
{
    public class RoleRequirementTestHelper
    {
        private IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());

        public AuthorizationHandlerContext context = null!;
        public RoleRequirement requirement = null!;
        public RoleRequirementHandlerReplace handler = null!;
        public Mock<IMediator> userMock = new Mock<IMediator>();

        public RoleRequirementTestHelper()
        {
            RecreateContext([]);
        }

        public void RecreateContext(List<RoleRequirement> requirements)
        {
            var contextBase = _fixture.Build<AuthorizationHandlerContext>().Create();
            var newClaims = contextBase.User.Claims.ToList();
            newClaims.Add(new Claim("user", Guid.NewGuid().ToString()));

            var newIdentity = new ClaimsIdentity(newClaims, "TestAuth");
            var newUser = new ClaimsPrincipal(newIdentity);

            context = new AuthorizationHandlerContext(
                requirements : requirements,
                newUser,
                contextBase.Resource
            );

            userMock = new Mock<IMediator>();
        }

        public async Task StartHandler()
        {
            handler = new RoleRequirementHandlerReplace(userMock.Object);
            await handler.PublicHandleRequirementAsync(context, requirement);
        }

    }
}
