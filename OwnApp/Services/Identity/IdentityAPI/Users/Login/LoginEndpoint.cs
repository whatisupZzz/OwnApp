using BuildingBlocks.Response;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Users.Login
{

    public record LoginRequest(string PhoneNumber, string CmsCode);
    public record LoginResponse(string Token, string RefreshToken);

    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (ISender sender, LoginRequest request) =>
            {
                var command = request.Adapt<LoginCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<LoginResponse>();
                return new { Code = 200, Data = response };
            }).WithName("Login");
        }
    }
}
