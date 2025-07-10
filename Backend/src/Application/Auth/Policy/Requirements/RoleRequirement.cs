using Domain.Entities.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Application.Auth.Policy.Requirements
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; set; } = string.Empty;

        public RoleRequirement(string roleName) => RoleName = roleName;
    }
}
