using Application.CQRS.Auth.Commands.CreateRole;
using Application.CQRS.Auth.Commands.RegisterUser;
using Application.CQRS.Auth.Commands.SetRoleForUser;
using Application.CQRS.Auth.Queries.GetUsersWithRoles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Auth;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = "RoleAdmin")]
    public class AdminController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AdminController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task RegisterUser([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegisterUserCommand>(request);

            await _mediator.Send(command);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersWithRolesRequest request, CancellationToken cancellationToken)
        {
            var command = new GetUsersWithRolesQuery(
                request.Page,
                request.Size
            );

            var result = await _mediator.Send(command, cancellationToken);

            return FormResultFromService(result);
        }

        [HttpPost("role")]
        public async Task AddNewRole([FromBody] AddRoleRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateRoleCommand(
                request.Name,
                request.NameRu,
                request.Description
            );

            await _mediator.Send(command);
        }

        [HttpPost("setroleforuser")]
        public async Task SetRoleForUser([FromBody] SetRoleForUserRequest request, CancellationToken cancellationToken)
        {
            var command = new SetRoleForUserCommand(
                request.userId,
                request.roleId
            );

            await _mediator.Send(command);
        }

    }
}
