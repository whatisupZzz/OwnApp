using BuildingBlocks.CQRS;
using BuildingBlocks.JWT.Interfaces;

namespace IdentityAPI.Users.GetUserInfo
{

    public record GetUserInfoQuery() : IQuery<GetUserInfoResult>;
    public record GetUserInfoResult(UserInfo User);

    public class GetUserInfoHandler(IUserRepository userRepository, IIdentityService identityService) : IQueryHandler<GetUserInfoQuery, GetUserInfoResult>
    {
        public async Task<GetUserInfoResult> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserIdentity();
            var user = await userRepository.GetUserByIdAsync(userId);
            return new GetUserInfoResult(user.ToUserInfo());

        }
    }
    public static class UserExtensions
    {
        public static UserInfo ToUserInfo(this User user)
        {
            return new UserInfo
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed
            };
        }
    }

}
