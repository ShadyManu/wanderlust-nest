using Application.Dto;
using Carter;
using Mapster;
using Microsoft.AspNetCore.HttpOverrides;
using Application;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureDependencyInjection(builder.Configuration);

builder.Services.AddApplicationDependencyInjection();
builder.Services.AddMapster();
MapsterConfig.Configure();

builder.Services.AddCarter();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", b => b
        .WithOrigins(["http://localhost:4200", "http://host.docker.internal:4200"])
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("MyPolicy");
    using var scope = app.Services.CreateScope();
    await using ApplicationDbContext db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapFallbackToFile("index.html");

app.UseForwardedHeaders();

app.UseRouting();

app.UseExceptionHandler(options => { });

app.MapCarter(); // Will add with reflection all endpoints which extend the class CarterModule

app.Run();

public partial class Program { }