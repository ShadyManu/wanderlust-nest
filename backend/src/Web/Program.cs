using CCTemplate.Infrastructure.Data;
using CCTemplate.Web.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

//builder.Services.

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapFallbackToFile("index.html");
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHealthChecks("/health");

app.MapRazorPages();

app.UseForwardedHeaders();
app.UseCors("MyPolicy");


app.UseRouting();
app.ConfigureAuth();
app.ConfigureEndpoints();

app.UseExceptionHandler(options => { });

app.Run();

public partial class Program { }

public static class WebAppExtensions
{
    public static void ConfigureAuth(this WebApplication app)
    {
        //app.UseAuthentication();
        //app.UseAuthorization();
    }

    public static void ConfigureEndpoints(this WebApplication app)
    {
        app.Map("/", () => Results.Redirect("/api"));
        app.MapEndpoints();
        //app.MapControllers();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");
    }
}
