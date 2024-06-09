using POC.Authentication.Domain.Entities;

namespace POC.Authentication.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(long id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
    }
}
