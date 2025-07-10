using Application.CQRS.Result.CQResult;
using Domain.Entities.Auth;
using MediatR;

namespace Application.CQRS.Auth.Queries.GetUserById
{
    public record GetUserByIdQuery
    (
        Guid Id,
        bool IsAddRole = false
    )
    : IRequest<CQResult<User>>;
}
