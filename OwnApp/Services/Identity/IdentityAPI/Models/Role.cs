namespace IdentityAPI.Models
{
    public class Role
    {
        [Key]
        public required string Id { get; set; }
        public string Name { get; set; } = string.Empty;    // e.g. "Admin", "User"
        public string? Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }

}
