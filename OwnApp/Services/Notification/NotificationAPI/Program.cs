using BuildingBlocks.Behaviors;
using BuildingBlocks.Core;
using BuildingBlocks.MassTransit;
using BuildingBlocks.Message.PersistMessage;
using BuildingBlocks.Web;
using Notification;
using Notification.Events.Consumers;
using Notification.Grpc;
using Notification.GrpcServer.Services;
using NotificationAPI;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
builder.AddServiceDefaults();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidatorBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddScoped<ISmsService, SmsService>();
//Grpc Services
builder.Services.AddGrpc();
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
builder.Services.AddScoped<IEventMapper, NotificationEventMapper>();
builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.AddPersistMessageProcessor(nameof(PersistMessage));
builder.Services.AddCustomMassTransit(builder.Environment, TransportType.RabbitMq, typeof(NotificationRoot).Assembly);

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGrpcService<NotificationGrpcService>();
app.UseHttpsRedirection();


app.Run();


