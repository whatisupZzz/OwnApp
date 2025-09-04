namespace IdentityAPI.Models
{
    public class Permission
    {
        [Key]
        public required string Id { get; set; }
        public string Name { get; set; } = "";  
        public string? Description { get; set; }
    }

}
