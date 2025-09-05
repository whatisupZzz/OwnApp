var builder = DistributedApplication.CreateBuilder(args);

var pgUsername = builder.AddParameter("pg-username", "postgres", secret: true);
var pgPassword = builder.AddParameter("pg-password", "postgres", secret: true);

var postgres = builder.AddPostgres("postgres", pgUsername, pgPassword)
    .WithImage("postgres:latest")
    .WithEndpoint(
        "tcp",
        e =>
        {
            e.Port = 5432;
            e.TargetPort = 5432;
            e.IsProxied = true;
            e.IsExternal = false;
        })
    .WithArgs(
        "-c",
        "wal_level=logical",
        "-c",
        "max_prepared_transactions=10");

var rabbitmqUsername = builder.AddParameter("rabbitmq-username", "guest", secret: true);
var rabbitmqPassword = builder.AddParameter("rabbitmq-password", "guest", secret: true);

var rabbitMq = builder.AddRabbitMQ("rabbitmq", rabbitmqUsername, rabbitmqPassword)
    .WithManagementPlugin()
    .WithEndpoint(
        "tcp",
        e =>
        {
            e.TargetPort = 5672;
            e.Port = 5672;
            e.IsProxied = true;
            e.IsExternal = false;
        })
    .WithEndpoint(
        "management",
        e =>
        {
            e.TargetPort = 15672;
            e.Port = 15672;
            e.IsProxied = true;
            e.IsExternal = true;
        });

if (builder.ExecutionContext.IsPublishMode)
{
    rabbitMq.WithLifetime(ContainerLifetime.Persistent);
}
if (builder.ExecutionContext.IsPublishMode)
{
    postgres.WithDataVolume("postgres-data")
        .WithLifetime(ContainerLifetime.Persistent);
}
var identityDb = postgres.AddDatabase("identity");
var notificationDb = postgres.AddDatabase("notification");
var persistMessageDb = postgres.AddDatabase("persist-message");

var identityapi =  builder.AddProject<Projects.IdentityAPI>("identityapi")
    .WithReference(identityDb)
    .WaitFor(identityDb)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq).WithReference(persistMessageDb)
    .WaitFor(persistMessageDb);

builder.AddProject<Projects.ApiGateWayAPI>("apigatewayapi");

builder.AddProject<Projects.NotificationAPI>("notificationapi")
        .WithReference(notificationDb)
        .WaitFor(notificationDb)
        .WithReference(rabbitMq)
        .WaitFor(rabbitMq).WithReference(persistMessageDb)
    .WaitFor(persistMessageDb);

builder.Build().Run();
