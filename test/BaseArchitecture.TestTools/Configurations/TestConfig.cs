using Autofac;
using Autofac.Extensions.DependencyInjection;
using BaseArchitecture.ApplicationServices.Employees;
using BaseArchitecture.Infrastructures.InternalMessaging;
using BaseArchitecture.Persistence.EF;
using BaseArchitecture.Persistence.EF.Employees;
using BaseArchitecture.TestTools.Configurations.Tools;
using Framework.Domain;
using Framework.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Xunit;

namespace BaseArchitecture.TestTools.Configurations
{
    [Collection("SequentialTests")]
    public class TestConfig : IDisposable
    {
        private readonly IContainer container;
        protected readonly EFDataContext writeDataContext;
        protected readonly EFDataContext readDataContext;
        private readonly string connectionString;

        public TestConfig()
        {
            connectionString = ConfigurationHelper
                .GetConfiguration()
                .GetSection("connectionString")
                .Value!;

            var builder = new ContainerBuilder();

            RegisterDependencies(builder);

            container = builder.Build();

            writeDataContext = container.Resolve<EFDataContext>();
            readDataContext = container.Resolve<EFDataContext>();
        }

        protected T Setup<T>()
        {
            return container.Resolve<T>();
        }

        public void Dispose()
        {
            using (var context = new EFDataContext(new DbContextOptionsBuilder<EFDataContext>()
                       .UseSqlServer(connectionString).Options))
            {
                context.Database.ExecuteSqlRaw(@"
                DECLARE @sql NVARCHAR(MAX) = N'';
                SELECT @sql += 'DELETE FROM [' + TABLE_SCHEMA + '].[' + TABLE_NAME + '];'
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_TYPE = 'BASE TABLE';
                EXEC sp_executesql @sql;");
            }
        }

        private void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(EFEmployeeRepository).Assembly)
                .As(typeof(Repository))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(RegisterEmployeeCommandHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.Register(c =>
                {
                    var options = new DbContextOptionsBuilder<EFDataContext>()
                        .UseSqlServer(connectionString)
                        .Options;
                    return new EFDataContext(options);
                })
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Populate(new ServiceCollection());
            builder.Register<MessageDispatcher>(c =>
                {
                    var serviceProvider = c.Resolve<IServiceProvider>();
                    return new MessageDispatcher(
                        serviceProvider,
                        typeof(IHandleMessage<>),
                        "Handle",
                        Assembly.Load("BaseArchitecture.ApplicationServices"));
                })
                .As<IMessageDispatcher>()
                .SingleInstance();
        }
    }
}
