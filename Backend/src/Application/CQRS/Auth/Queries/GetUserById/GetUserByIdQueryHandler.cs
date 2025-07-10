using Application.Abstractions.Repositories;
using Application.CQRS.Result.CQResult;
using Domain.Entities.Auth;
using MediatR;

namespace Application.CQRS.Auth.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IAuthRepository authRepository) : IRequestHandler<GetUserByIdQuery, CQResult<User>>
    {
        public async Task<CQResult<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new CQResult<User>();
            var user = await authRepository.GetUserByIdAsync(request.Id, request.IsAddRole);

            if (user != null)
            {
                result.SetResultData(user);
            }

            return result;
        }
    }

}
