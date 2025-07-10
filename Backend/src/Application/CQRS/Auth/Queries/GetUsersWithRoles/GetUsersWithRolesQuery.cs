using Application.CQRS.Result.CQResult;
using Application.DTO.Auth;
using Domain.Entities.Auth;
using MediatR;

namespace Application.CQRS.Auth.Queries.GetUsersWithRoles
{
    public record GetUsersWithRolesQuery
    (
       int Page,
       int Size
    )
    : IRequest<CQResult<List<UserWithRoleDto>>>;
}
