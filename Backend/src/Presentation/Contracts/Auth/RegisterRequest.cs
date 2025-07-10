using Domain.Entities;

namespace Presentation.Contracts.Auth
{
    public record RegisterRequest
    (
        string Login,
        string Password,
        string Email,
        string Surname,
        string Name,
        string Patronymic,
        Guid TeacherId = default
    );
}
