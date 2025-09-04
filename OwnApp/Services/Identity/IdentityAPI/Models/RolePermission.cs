namespace IdentityAPI.Models
{
    public class RolePermission
    {
        public required string RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public required string PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }

}
