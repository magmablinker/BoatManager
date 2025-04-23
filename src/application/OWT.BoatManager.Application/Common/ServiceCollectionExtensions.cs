using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OWT.BoatManager.Application.Abstractions;
using OWT.BoatManager.Application.UseCases.Boats.Commands.Validators;
using OWT.BoatManager.Application.UseCases.Boats.Models;

namespace OWT.BoatManager.Application.Common;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISender, Sender>()
            .AddRequestHandlers(AssemblyProvider.Current)
            .AddValidators();

        return services;
    }

    private static IServiceCollection AddRequestHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .Select(i => new
                {
                    Implementation = t,
                    Interface = i,
                }))
            .ToList();

        foreach (var handler in handlerTypes)
            services.AddScoped(handler.Interface, handler.Implementation);

        return services;
    }

    private static void AddValidators(this IServiceCollection services) =>
        services.AddScoped<IValidator<BoatModel>, BoatModelValidator>();
}
