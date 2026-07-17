using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zeal.Bootcamp.DnD.Application.Data;
using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;
using Zeal.Bootcamp.DnD.Data.Queries;

namespace Zeal.Bootcamp.DnD.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder>? configureDatabase = null)
    {
        services.AddDbContext<DnDContext>(options => configureDatabase?.Invoke(options));

        return services
            .AddScoped<IDatabase, DnDContext>()
            .AddScoped<IDataStore>(provider => provider.GetRequiredService<DnDContext>())
            .AddScoped<IListCharactersDataQuery, ListCharactersDataQuery>();
    }
}
