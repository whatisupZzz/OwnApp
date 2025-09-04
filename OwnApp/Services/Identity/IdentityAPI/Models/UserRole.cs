namespace IdentityAPI.Models
{
    public class UserRole
    {
        public required string UserId { get; set; }
        public User User { get; set; } = null!;

        public required string RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }

}
