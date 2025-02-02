var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BaseCleanArchitecture_API>("basecleanarchitecture-api");

builder.Build().Run();
