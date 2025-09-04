namespace IdentityAPI.Models
{
    public class RefreshToken
    {
        [Key]
        public required string Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public required string UserId { get; set; }
        public User User { get; set; } = null!;
    }

}
