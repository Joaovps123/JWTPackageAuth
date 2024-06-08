using MediatR;
using POC.Authentication.Application.DTOs;

namespace POC.Authentication.Application.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<AuthenticationResponseDto>
    {
        public string RefreshToken { get; set; }
    }
}
