using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zeal.Bootcamp.DnD.Application.Data.Queries.ListCharacters;
using Zeal.Bootcamp.DnD.Data.Queries;

namespace Zeal.Bootcamp.DnD.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IDatabase, DnDContext>()

            // Week 4
            .AddScoped<IListCharactersDataQuery, ListCharactersDataQuery>()
            .AddDbContext<DnDContext>(options =>
            {
                options.UseSqlite("Data Source=app.db");
            });
    }
}