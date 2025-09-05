using Basket.API.Exception;
using BuildingBlocks.Creator;

namespace IdentityAPI.Repository.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _dbContext;
        public UserRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetUserByPhoneAsync(string phone, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phone, cancellationToken);
            return user is null ? throw new UserNotFoundExcpetion(phone) : user;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user is null ? throw new UserNotFoundExcpetion(id) : user;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            return user is null ? throw new UserNotFoundExcpetion(userName) : user;
        }

        public async Task<User> CreateUserAsync(string phone, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                PhoneNumber = phone,
                CreatedAt = DateTime.Now,
                UserName = Creator.CreateTempUserName(),
            };
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<User> UpdateOrInsertUserAsync(string phone, string code, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phone, cancellationToken);
            User? newUser;
            if (user is null)
            {
                newUser = await CreateUserAsync(phone, cancellationToken);
                newUser.CmsCode = code;
                await _dbContext.SaveChangesAsync(cancellationToken);
                return newUser;
            }
            else
            {
                user.CmsCode = code;
                await _dbContext.SaveChangesAsync(cancellationToken);
                return user;
            }

        }
    }
}
