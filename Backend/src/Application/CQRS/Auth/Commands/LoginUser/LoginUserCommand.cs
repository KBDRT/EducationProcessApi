using Application.CQRS.Result.CQResult;
using MediatR;

namespace Application.CQRS.Auth.Commands.LoginUser
{
    public record LoginUserCommand
    (
        string Login,
        string Password

    ) : IRequest<CQResult<string>>;
}
