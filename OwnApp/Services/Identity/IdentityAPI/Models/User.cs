
namespace IdentityAPI.Models
{
    public class User
    {
        [Key]
        public required string  Id { get; set; }             
        public string? UserName { get; set; } = string.Empty;   
        public string? Email { get; set; } = string.Empty;      
        public string? PasswordHash { get; set; } = string.Empty; 
        public required string  PhoneNumber { get; set; }  
        public string? CmsCode { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? EmsCode { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastLoginAt { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
