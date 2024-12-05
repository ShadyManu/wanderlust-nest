using Application.Commons.Interfaces;
using WanderlustNest.Web.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

namespace WanderlustNest.Web;

public static class WebDependencyInjection
{
    public static void AddWebDependencyInjection(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy", b => b
                // .WithOrigins(["http://localhost:8100", "http://host.docker.internal:8100"])
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
        });

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
        
        services.AddScoped<IUser, CurrentUser>();
        services.AddHttpContextAccessor();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Type JWT (without Bearer) to authenticate'"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}