using POC.Authentication.Domain.Entities;

namespace POC.Authentication.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
        DateTime GetRefreshTokenExpiryDate();
    }
}
