using Autofac;
using BaseArchitecture.ApplicationServices.Employees;
using BaseArchitecture.Persistence.EF.Employees;
using Framework.Domain;

namespace _04.EndPoints.API.Configs;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder RegisterRepository(
        this WebApplicationBuilder builder)
    {
        builder
            .Host
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterAssemblyTypes(typeof(EFEmployeeRepository).Assembly)
                    .As(typeof(Repository))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            });

        return builder;
    }

    public static WebApplicationBuilder RegisterICommandHandler(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterAssemblyTypes(typeof(RegisterEmployeeHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();
        });

        return builder;
    }
}