namespace IdentityAPI.Data.Extensions;
internal class InitialData
{
    public static IEnumerable<User> Users =>
        new List<User>
        {
            new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                Email = "admin@example.com",
                PasswordHash = "12345", //
                PhoneNumber = "10000000000",
                CmsCode = "CMS001",
                EmsCode = "EMS001",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedAt = DateTime.Now,
                LastLoginAt = null
            },
            new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "johndoe",
                Email = "john.doe@example.com",
                PasswordHash = "12345",
                PhoneNumber = "19998887777",
                CmsCode = "CMS001",
                EmsCode = "EMS001",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                CreatedAt = DateTime.Now,
                LastLoginAt = null
            }
        };
}
