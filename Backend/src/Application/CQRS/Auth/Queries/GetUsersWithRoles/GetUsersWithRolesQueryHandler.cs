using Application.Abstractions.Repositories;
using Application.CQRS.Result.CQResult;
using Application.DTO.Auth;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Auth.Queries.GetUsersWithRoles
{
    public class GetUsersWithRolesQueryHandler(IAuthRepository authRepository,
                                               IMapper mapper)                  
                                               : IRequestHandler<GetUsersWithRolesQuery, CQResult<List<UserWithRoleDto>>>
    {

        public async Task<CQResult<List<UserWithRoleDto>>> Handle(GetUsersWithRolesQuery request, CancellationToken cancellationToken)
        {
            var result = new CQResult<List<UserWithRoleDto>>();

            var usersFull = await authRepository.GetUsersWithRolesPaginationAsync(request.Page, request.Size);
            var usersShort = mapper.Map<List<UserWithRoleDto>>(usersFull);
            result.SetResultData(usersShort);

            return result;
        }
    }

}
