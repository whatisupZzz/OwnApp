


using BuildingBlocks.Response;
using Mapster;

namespace IdentityAPI.Users.GetUserInfo
{

    public record GetUserInfoRequest() : IQuery<GetUserInfoResult>;
    public record GetUserInfoResponse(UserInfo User);
    public class UserInfo
    {
        public required string Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public required string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }
    public class GetUserInfoEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/getuserinfo", async (ISender sender, HttpContext context) =>
            {
                var result = await sender.Send(new GetUserInfoQuery());
                var response = result.Adapt<GetUserInfoResponse>();
                return new BaseResponse(response);
            }).WithName("getuserinfo").RequireAuthorization();
        }
    }
}
