using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeal.Bootcamp.DnD.Application.Pipeline;
using Zeal.Bootcamp.DnD.Application.Services.Catalogs;
using Zeal.Bootcamp.DnD.Application.UnitOfWorks;

namespace Zeal.Bootcamp.DnD.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblyContaining<AssemblyMarker>())
            .AddSingleton<IClassCatalog, ClassCatalog>()
            .AddSingleton<IWeaponCatalog, WeaponCatalog>()
            .AddScoped<IDomainEventsStorage, DomainEventsStorage>()
            .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
            .AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddMediatrBehaviors(this IServiceCollection services)
        => services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>))
            .AddScoped<UnitOfWorkBehaviorState>()
            ;
}
