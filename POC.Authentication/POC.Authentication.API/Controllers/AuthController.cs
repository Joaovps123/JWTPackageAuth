using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.Authentication.Application.Commands.ChangePassword;
using POC.Authentication.Application.Commands.LoginUser;
using POC.Authentication.Application.Commands.RefreshToken;
using POC.Authentication.Application.Commands.RegisterUser;
using POC.Authentication.Application.DTOs;

namespace POC.Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
                return Ok("User registered successfully.");
            return BadRequest("User registration failed.");
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResponseDto), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result != null)
                return Ok(result);
            return Unauthorized();
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(AuthenticationResponseDto), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            if (result != null)
                return Ok(result);
            return Unauthorized();
        }

        [Authorize]
        [HttpPost("change-password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            command.Email = User.Identity.Name;

            var result = await _mediator.Send(command);
            if (result)
                return Ok();
            return BadRequest();
        }

        [Authorize]
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
                return Ok("Ok");
        }
    }
}
