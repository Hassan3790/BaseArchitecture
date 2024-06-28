using Autofac;
using BaseArchitecture.ApplicationServices.Employees;
using BaseArchitecture.Persistence.EF.Employees;
using Framework.Domain;
using Framework.Domain.Events;
using Microsoft.AspNetCore.Builder;

namespace BaseArchitecture.Infrastructures.Configs;

public static class DependencyInjectionConfig
{
    public static ContainerBuilder RegisterRepository(
        this ContainerBuilder containerBuilder)
    {

        containerBuilder.RegisterAssemblyTypes(typeof(EfEmployeeWriteRepository).Assembly)
                    .As(typeof(WriteRepository))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

        containerBuilder.RegisterAssemblyTypes(typeof(EfEmployeeReadRepository).Assembly)
                    .As(typeof(ReadRepository))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

        return containerBuilder;
    }

    public static ContainerBuilder RegisterICommandHandler(this ContainerBuilder containerBuilder)
    {

        containerBuilder.RegisterAssemblyTypes(typeof(RegisterEmployeeCommandHandler).Assembly)
            .AsClosedTypesOf(typeof(ICommandHandler<>))
            .InstancePerLifetimeScope();

        return containerBuilder;
    }

    public static ContainerBuilder RegisterMessageHandler(this ContainerBuilder containerBuilder)
    {

        containerBuilder.RegisterAssemblyTypes(typeof(RegisterEmployeeCommandHandler).Assembly)
            .AsClosedTypesOf(typeof(IHandleMessage<>))
            .InstancePerLifetimeScope();

        return containerBuilder;
    }
}