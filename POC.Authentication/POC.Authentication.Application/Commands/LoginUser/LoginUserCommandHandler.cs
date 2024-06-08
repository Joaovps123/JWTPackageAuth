using MediatR;
using POC.Authentication.Application.DTOs;
using POC.Authentication.Application.Interfaces;
using POC.Authentication.Domain.Interfaces;

namespace POC.Authentication.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthenticationResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginUserCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthenticationResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            var token = _jwtTokenService.GenerateToken(user);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            var refreshTokenEntity = new Domain.Entities.RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                Expires = _jwtTokenService.GetRefreshTokenExpiryDate()
            };

            await _userRepository.AddRefreshTokenAsync(refreshTokenEntity);

            return new AuthenticationResponseDto
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }
}
