using Api.Configurations;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Models.IdentityModels;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Host.ConfigureMyLogging();
builder.Services.AddAutoMapper(typeof(MapperInitializer));
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigurePolicies();
builder.Services.ConfigureAppCookies();
builder.Services.ConfigureDependencies();
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<DataContext>()
    .AddApiEndpoints();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<ApplicationUser>();
app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}")
    .RequireAuthorization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
