using Application.Auth.Policy.Requirements;
using Application.CQRS.Auth.Queries.GetUserById;
using Application.CQRS.Result.CQResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Application.Auth.Policy.UploadEduPlan
{
    public class PermissionRequirementHandler(IMediator mediator) : AuthorizationHandler<PermissionRequirement>
    {
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var claimUserId = context.User?.FindFirst(c => c.Type == "user");
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

                var requiredPermission = user?.Roles
                    .Where(p => p.Permissions.Any(x => x.TargetForAction == requirement.Target && x.Action == requirement.Action))
                    .SingleOrDefault();
                if (requiredPermission != null)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            context.Fail();
        }


    }
}
