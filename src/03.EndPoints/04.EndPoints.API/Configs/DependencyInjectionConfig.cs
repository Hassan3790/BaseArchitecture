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
                containerBuilder.RegisterAssemblyTypes(typeof(EfEmployeeWriteRepository).Assembly)
                    .As(typeof(WriteRepository))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            });

        hostBuilder
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterAssemblyTypes(typeof(EfEmployeeReadRepository).Assembly)
                    .As(typeof(ReadRepository))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            });

        return hostBuilder;
    }

    public static ConfigureHostBuilder RegisterICommandHandler(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterAssemblyTypes(typeof(RegisterEmployeeCommandHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();
        });

        return hostBuilder;
    }
}