
using Application.CQRS.Result.CQResult;
using MediatR;

namespace Application.CQRS.Auth.Commands.RegisterUser
{
    public record RegisterUserCommand
    (
        string Login,
        string Password,
        string Email,
        string Surname,
        string Name,
        string Patronymic,
        Guid TeacherId = default

    ) : IRequest<CQResult<Guid>>;
}
