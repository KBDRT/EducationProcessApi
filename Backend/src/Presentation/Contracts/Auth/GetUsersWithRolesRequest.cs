namespace Presentation.Contracts.Auth
{
    public record GetUsersWithRolesRequest
    (
        int Page,
        int Size = 25
    );
}
