namespace POC.Authentication.Domain.Entities
{
    public class RefreshToken
    {
        public long Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public long UserId { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
        public User User { get; set; }
    }
}
