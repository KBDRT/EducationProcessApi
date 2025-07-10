
namespace Application.DTO.Auth
{
    public class UserWithRoleDto
    {
        public Guid UserId { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Initials { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<RoleDto> Roles { get; set; } = [];

    };

    public class RoleDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string RoleNameRu { get; set; } = string.Empty;
        public string RoleDescription { get; set; } = string.Empty;
    };

}
