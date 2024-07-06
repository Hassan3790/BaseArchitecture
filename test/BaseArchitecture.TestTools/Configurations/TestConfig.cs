using Autofac;
using Autofac.Extensions.DependencyInjection;
using BaseArchitecture.ApplicationServices.Employees;
using BaseArchitecture.Persistence.EF;
using BaseArchitecture.Persistence.EF.Employees;
using BaseArchitecture.TestTools.Configurations.Tools;
using Framework.Domain;
using Framework.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BaseArchitecture.Infrastructures.Jobs;
using BaseArchitecture.Persistence.EF.OutboxMessages;
using Xunit;

namespace BaseArchitecture.TestTools.Configurations
{
    [Collection("SequentialTests")]
    public class TestConfig : IDisposable
    {
        private readonly IContainer container;
        protected readonly ApplicationDbContext writeDataContext;
        protected readonly ApplicationDbContext readDataContext;
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

            writeDataContext = container.Resolve<ApplicationDbContext>();
            readDataContext = container.Resolve<ApplicationDbContext>();
        }

        protected T Setup<T>()
        {
            return container.Resolve<T>();
        }

        public void Dispose()
        {
            using (var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
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
            builder.RegisterAssemblyTypes(typeof(EfEmployeeWriteRepository).Assembly)
                .As(typeof(WriteRepository))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(EfEmployeeReadRepository).Assembly)
                .As(typeof(ReadRepository))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof(EfUnitOfWork))
                .As(typeof(UnitOfWork))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof(OutboxManagement))
                .As(typeof(IOutboxManagement))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(RegisterEmployeeCommandHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.Register(c =>
                {
                    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseSqlServer(connectionString)
                        .Options;
                    return new ApplicationDbContext(options);
                })
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Populate(new ServiceCollection());
            builder.Register<MessageDispatcher>(c =>
                {
                    var serviceProvider = c.Resolve<IServiceProvider>();
                    return new MessageDispatcher(serviceProvider);
                })
                .As<IMessageDispatcher>()
                .SingleInstance();
        }
    }
}
