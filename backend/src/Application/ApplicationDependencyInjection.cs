using System.Reflection;
using Application.Commons.Behaviors;
using Application.Commons.Interfaces.Services;
using Application.Dto;
using Application.Services;
using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application;

public static class ApplicationDependencyInjection
{
    public static void AddApplicationDependencyInjection(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        
        // Registriamo IHttpClientFactory per HttpClient
        services.AddHttpClient();

        // Registriamo il servizio OpenAIService
        services.TryAddScoped<IAgentAIService, OpenAIService>();
        
        services.AddMapster();
        MapsterConfig.Configure();

        services.AddCarter();
    }
}