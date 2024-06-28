using BaseArchitecture.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BaseArchitecture.Infrastructures.Configs;

public static class EFDataContextConfig
{
    public static IServiceCollection RegisterDbContext(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<EFDataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}