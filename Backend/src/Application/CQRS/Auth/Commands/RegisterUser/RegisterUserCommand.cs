
using Application.CQRS.Result.CQResult;
using MediatR;

namespace Application.CQRS.Auth.Commands.RegisterUser
{
    public record RegisterUserCommand
    (
        string Login,
        string Password

    ) : IRequest<CQResult<Guid>>;
}
