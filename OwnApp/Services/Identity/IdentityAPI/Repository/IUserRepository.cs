
namespace IdentityAPI.Repository.Repo
{
    public interface IUserRepository
    {
        Task<User> GetUserByPhoneAsync(string phone, CancellationToken cancellationToken);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User> CreateUserAsync(string phone, CancellationToken cancellationToken);
    }
}
