
using BuildingBlocks.Response;
using Mapster;

namespace IdentityAPI.Users.Login
{




    public record SendTheSmsRequest(string PhoneNumber, int SmsType);
    public record SendSmsResponse(bool IsSuccess);
    public class SendSmsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/sendsms", async (ISender sender, SendTheSmsRequest request) =>
            {
                var command = request.Adapt<SendSmsCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<SendSmsResponse>();
                return new BaseResponse(response);
            }).WithName("sendsms");
        }
    }
}
