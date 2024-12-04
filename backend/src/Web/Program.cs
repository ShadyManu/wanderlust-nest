using Carter;
using Application;
using WanderlustNest.Web;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureDependencyInjection(builder.Configuration);
builder.Services.AddApplicationDependencyInjection();
builder.Services.AddWebDependencyInjection();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("MyPolicy");
    
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = string.Empty; // Swagger will be available at http://localhost:8080/index.html
    });
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Configuration.GetValue<bool>("Database:ApplyMigration"))
{
    await app.MigrateAsync();
}

if (app.Configuration.GetValue<bool>("Database:SeedData"))
{
    await app.SeedAsync();
}

app.MapFallbackToFile("index.html");

app.UseForwardedHeaders();

app.UseRouting();

app.UseExceptionHandler(options => { });

app.MapCarter(); // Will add with reflection all endpoints which extend the class 

app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<User>();

app.Run();

public partial class Program { }