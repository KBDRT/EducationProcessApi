using Application.Auth.Policy.Requirements;
using Application.CQRS.Auth.Queries.GetUserById;
using Application.CQRS.Result.CQResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Application.Auth.Policy.UploadEduPlan
{
    public class RoleRequirementHandler(IMediator mediator) : AuthorizationHandler<RoleRequirement>
    {
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var claimUserId = context.User.FindFirst(c => c.Type == "user");
            if (claimUserId == null || !Guid.TryParse(claimUserId.Value, out Guid userId))
            {
                context.Fail();
                return;
            }

            var result = await mediator.Send(new GetUserByIdQuery(userId, true));
            if (result != null && result.ResultCode == CQResultStatusCode.Success)
            {
                var user = result.ResultData;
                if (user == null)
                {
                    context.Fail();
                    return;
                }

                if (user.Roles.Any(x => x.Name == "Admin"))
                {
                    context.Succeed(requirement);
                    return;
                }

                var requiredRole = user?.Roles
                    .SingleOrDefault(x => x.Name == requirement.RoleName);
                if (requiredRole != null)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            context.Fail();
        }


    }
}
