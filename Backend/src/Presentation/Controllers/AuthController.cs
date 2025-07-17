using Application.CQRS.Auth.Commands.LoginUser;
using Application.CQRS.Result.CQResult;
using Application.Settings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentation.Contracts.Auth;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IOptions<AuthSettings> _authSettings;

        public AuthController(IMediator mediator, 
                              IOptions<AuthSettings> authSettings)
        {
            _mediator = mediator;
            _authSettings = authSettings;
        }

        [HttpGet("/checkauth")]
        public async Task<IActionResult> CheckAuth()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return Ok();
            }
            return Unauthorized();
        }


        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete(_authSettings.Value.CookieNameForToken);
            return Ok();
        }


        [HttpPost("/login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request, 
                                                    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new LoginUserCommand(request.Login, request.Password));

            if (result.ResultCode == CQResultStatusCode.Success && result.ResultData != null)
            {
                Response.Cookies.Append(_authSettings.Value.CookieNameForToken, result.ResultData,
                new CookieOptions
                {
                    Expires = DateTime.UtcNow.Add(_authSettings.Value.TokenLifeTime),
                });

                return Ok(request.Login);
            }

            return NotFound();

        }

    }
}
