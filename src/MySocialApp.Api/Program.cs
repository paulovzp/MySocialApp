using MySocialApp.Api.AuthOverrides;
using MySocialApp.Api.Extensions;
using MySocialApp.Api.Middlewares;
using MySocialApp.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMySocialAppAuthentication();
builder.Services.AddMySocialAppSwagger();
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddScoped<IUserSession, UserSession>();

var app = builder.Build();

app.UseMySocialAppSwagger();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseMySocialAppAuthentication();
app.MapControllers();

app.Run();
