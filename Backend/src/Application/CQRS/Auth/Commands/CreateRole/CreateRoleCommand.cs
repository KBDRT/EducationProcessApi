using Application.CQRS.Result.CQResult;
using Domain.Entities.Auth;
using MediatR;

namespace Application.CQRS.Auth.Commands.CreateRole
{
    public record CreateRoleCommand
    (
        string Name,
        string NameRu,
        string Description

    ) : IRequest<CQResult<Guid>>;
}
