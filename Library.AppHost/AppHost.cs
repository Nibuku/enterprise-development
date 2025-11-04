var builder = DistributedApplication.CreateBuilder(args);

var mongoDb = builder.AddMongoDB("mongo").AddDatabase("library");

builder.AddProject<Projects.Library_Api>("api")
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

builder.Build().Run();
