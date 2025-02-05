using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Zeal.Bootcamp.DnD.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IDatabase, DnDContext>()
            .AddDbContext<DnDContext>(options =>
            {
                options.UseSqlite("Data Source=app.db");
            });
    }
}