using Autofac;
using BaseArchitecture.ApplicationServices.Employees;
using BaseArchitecture.Persistence.EF.Employees;
using Framework.Domain;

namespace _04.EndPoints.API.Configs;

public static class DependencyInjectionConfig
{
    public static ConfigureHostBuilder RegisterRepository(
        this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterAssemblyTypes(typeof(EFEmployeeRepository).Assembly)
                    .As(typeof(Repository))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            });

        return hostBuilder;
    }

    public static ConfigureHostBuilder RegisterICommandHandler(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterAssemblyTypes(typeof(RegisterEmployeeHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();
        });

        return hostBuilder;
    }
}