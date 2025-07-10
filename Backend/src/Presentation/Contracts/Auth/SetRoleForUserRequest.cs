namespace Presentation.Contracts.Auth
{
    public record SetRoleForUserRequest
    (
        Guid userId,
        Guid roleId
    );
}
