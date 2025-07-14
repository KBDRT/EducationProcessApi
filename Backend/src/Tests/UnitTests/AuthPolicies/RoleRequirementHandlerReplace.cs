

using Application.Auth.Policy.Requirements;
using Application.Auth.Policy.UploadEduPlan;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Tests.UnitTests.AuthPolicies
{
    public class RoleRequirementHandlerReplace(IMediator mediator) : RoleRequirementHandler(mediator)
    {

        public Task PublicHandleRequirementAsync(AuthorizationHandlerContext context,
                                                 RoleRequirement requirement)
        {
            return base.HandleRequirementAsync(context, requirement);
        }
    }
}
