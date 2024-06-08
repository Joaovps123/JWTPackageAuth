using MediatR;
using POC.Authentication.Application.DTOs;

namespace POC.Authentication.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<AuthenticationResponseDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
