using Application.Dtos;
using Carter;
using Mapster;
using Microsoft.AspNetCore.HttpOverrides;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
    [
        Assembly.GetExecutingAssembly(), // Assembly Web
        Assembly.Load("Application")    // Application Assembly
    ]);
});


builder.Services.AddMapster(); // TODO: check if we can remove Mapster dependency and leave just MapsterDependencyInjection
MapsterConfig.Configure();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder => builder
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