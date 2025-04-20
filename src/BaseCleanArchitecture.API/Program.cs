using BaseCleanArchitecture.API.Controllers;
using BaseCleanArchitecture.API.Handler;
using BaseCleanArchitecture.API.Modules;
using BaseCleanArchitecture.Application;
using BaseCleanArchitecture.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services
    .AddControllers()
    .AddOData(options =>
    {
        options
        .Select()
        .Filter()
        .Count()
        .Expand()
        .OrderBy()
        .SetMaxTop(null)
        .AddRouteComponents("odata", AppODataController.GetEdmModel());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// DI
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", options =>
    {
        options.QueueLimit = 100;
        options.Window = TimeSpan.FromMinutes(1);
        options.PermitLimit = 100;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseExceptionHandler();

app.UseCors(options =>
{
    options
    .AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod()
    .SetIsOriginAllowed(x => true);
});

app.RegisterRoutes();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("fixed");

app.Run();
