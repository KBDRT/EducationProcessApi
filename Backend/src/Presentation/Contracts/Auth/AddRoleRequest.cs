using Domain.Entities.Auth;

namespace Presentation.Contracts.Auth
{
    public record AddRoleRequest
    (
        string Name,
        string NameRu,
        string Description
    );
}
