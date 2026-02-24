using RestaurantManager.Api;
using RestaurantManager.Application;
using RestaurantManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddWeb(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.BuildMiddlewaresPipeline();

app.Run();
