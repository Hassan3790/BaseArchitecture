using BaseArchitecture.ApplicationServices.Employees;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Infrastructures.EF.Employees;

namespace _04.EndPoints.API.Configs;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterDependency(
        this IServiceCollection services)
    {
        services.AddScoped<RegisterEmployeeHandler>();
        services.AddScoped<EmployeeRepository, EFEmployeeRepository>();

        return services;
    } 
}