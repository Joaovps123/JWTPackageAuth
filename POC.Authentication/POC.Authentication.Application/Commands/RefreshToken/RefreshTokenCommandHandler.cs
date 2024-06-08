using MediatR;
using POC.Authentication.Application.DTOs;
using POC.Authentication.Application.Interfaces;
using POC.Authentication.Domain.Interfaces;

namespace POC.Authentication.Application.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticationResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public RefreshTokenCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthenticationResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _userRepository.GetRefreshTokenAsync(request.RefreshToken);
            if (refreshToken == null || refreshToken.IsExpired)
                return null;

            var user = await _userRepository.GetUserByEmailAsync(refreshToken.User.Email);

            var newJwtToken = _jwtTokenService.GenerateToken(user);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

            refreshToken.Token = newRefreshToken;
            refreshToken.Expires = _jwtTokenService.GetRefreshTokenExpiryDate();

            await _userRepository.UpdateRefreshTokenAsync(refreshToken);

            return new AuthenticationResponseDto
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
