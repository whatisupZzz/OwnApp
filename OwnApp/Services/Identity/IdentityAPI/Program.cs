
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.JWT;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidatorBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<AuthDbContext>("identitydb");
builder.Services.AddCarter();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddExceptionHandler<CustomExpectionHandler>();
builder.Services.AddJwt(builder.Configuration);
var app = builder.Build();

app.MapDefaultEndpoints();

app.UseAuthentication();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
app.MapCarter();
app.UseExceptionHandler(config => { });
app.Run();


