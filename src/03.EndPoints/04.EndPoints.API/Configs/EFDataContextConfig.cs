using BaseArchitecture.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace _04.EndPoints.API.Configs;

public static class EFDataContextConfig
{
    public static IServiceCollection RegisterDbContext(
        this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        var connectionString =
            configurationManager
                .GetValue<string>("connectionString");
        
        services.AddDbContext<EFDataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}