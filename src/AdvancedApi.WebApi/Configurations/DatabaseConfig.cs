using AdvancedApi.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AdvancedApi.WebApi.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        var connectionString = configuration.GetConnectionString("AdvancedConnection");
        services.AddDbContext<AdvancedContext>(opt => opt.UseSqlServer(connectionString));
    }
}