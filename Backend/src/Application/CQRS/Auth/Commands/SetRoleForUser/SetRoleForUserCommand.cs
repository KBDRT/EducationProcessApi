using Application.CQRS.Result.CQResult;
using Domain.Entities.Auth;
using MediatR;

namespace Application.CQRS.Auth.Commands.SetRoleForUser
{
    public record SetRoleForUserCommand
    (
        Guid userId,
        Guid roleId

    ) : IRequest<CQResult>;
}
