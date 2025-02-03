using Microsoft.AspNetCore.Authentication.JwtBearer;
using RoadLink.Application;
using RoadLink.Application.Abstractions.Authentication;
using RoadLink.Infrastructure;
using RoadLink.Infrastructure.Authentication;
using RoatLink.Api.Extensions;
using RoatLink.Api.OptionsSetup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddTransient<IJwtProvider, JwtProvider>();
builder.Services.AddAuthorization();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

await app.ApplyMigrations();
app.SeedData();
app.SeedDataAuthentication();

app.UseCustomExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

