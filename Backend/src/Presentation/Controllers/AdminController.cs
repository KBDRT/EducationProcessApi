using Amazon.S3;
using Application.CQRS.Analysis.Commands.CreateFileForDocument;
using Application.CQRS.Auth.Commands.CreateRole;
using Application.CQRS.Auth.Commands.RegisterUser;
using Application.CQRS.Auth.Commands.SetRoleForUser;
using Application.CQRS.Auth.Queries.GetUsersWithRoles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Presentation.Contracts.Auth;
using System.Runtime.Intrinsics.X86;
using System.Security.AccessControl;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = "RoleAdmin")]
    public class AdminController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IMinioClient _minioClient;

        public AdminController(IMediator mediator, IMapper mapper, IMinioClient minioClient)
        {
            _mediator = mediator;
            _mapper = mapper;
            _minioClient = minioClient;
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
