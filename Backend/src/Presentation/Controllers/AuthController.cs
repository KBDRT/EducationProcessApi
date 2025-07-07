using Application.CQRS.Analysis.Commands.CreateCriteria;
using Application.CQRS.Auth.Commands.LoginUser;
using Application.CQRS.Auth.Commands.RegisterUser;
using Application.CQRS.Result.CQResult;
using EducationProcess.Presentation.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Presentation.Contracts.Auth;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("/register")]
        [Authorize]
        public async Task RegisterUser([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RegisterUserCommand(request.Login, request.Password));
        }



        [HttpGet("/checkauth")]
        public async Task<IActionResult> RegisterUser()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return Ok("User");
            }
            return Unauthorized();
        }



        [HttpPost("/login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request, 
                                                    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new LoginUserCommand(request.Login, request.Password));


            if (result.ResultCode == CQResultStatusCode.Success && result.ResultData != null)
            {
                Response.Cookies.Append("token", result.ResultData,
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromMinutes(60)
                });

                return Ok();
            }

            return NotFound();

        }

    }
}
