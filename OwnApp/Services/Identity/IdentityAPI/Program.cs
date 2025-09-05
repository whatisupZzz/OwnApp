using BuildingBlocks.Behaviors;
using BuildingBlocks.Core;
using BuildingBlocks.EFCore;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.JWT;
using BuildingBlocks.JWT.Interfaces;
using BuildingBlocks.MassTransit;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidatorBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
builder.Services.AddScoped<IEventMapper, IdentityEventMapper>();
builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
builder.AddServiceDefaults();
//builder.AddNpgsqlDbContext<AuthDbContext>("identitydb");
builder.Services.AddCarter();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddExceptionHandler<CustomExpectionHandler>();
builder.Services.AddJwt(builder.Configuration);
builder.AddPersistMessageProcessor(nameof(PersistMessage));
builder.AddCustomDbContext<AuthDbContext>(nameof(Identity));

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddCustomMassTransit(builder.Environment, TransportType.RabbitMq, typeof(IdentityRoot).Assembly);

//Grpc Services
builder.Services.AddGrpcClient<NotificationService.NotificationServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});
var app = builder.Build();

app.MapDefaultEndpoints();

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
app.MapCarter();
app.UseExceptionHandler(config => { });
app.Run();


public class Identity { }
