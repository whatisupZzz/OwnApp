var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")    
    .WithImage("ankane/pgvector")
    .WithImageTag("latest")
    //.WithEndpoint(name:"pg-tcp", port: 15432, targetPort: 5432)
    //.WithBindMount("D:\\doker\\doker_volumns", "/var/lib/postgresql/data")
    .WithLifetime(ContainerLifetime.Persistent).WithHostPort(15432);

var identityDb = postgres.AddDatabase("identitydb");

var identityapi =  builder.AddProject<Projects.IdentityAPI>("identityapi")
    .WithReference(identityDb)
    .WaitFor(identityDb);

builder.AddProject<Projects.ApiGateWayAPI>("apigatewayapi");

builder.Build().Run();
