using Domain.Entities.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Application.Auth.Policy.Requirements
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionAction Action { get; set; } = PermissionAction.None;

        public string Target = string.Empty;
    }
}
