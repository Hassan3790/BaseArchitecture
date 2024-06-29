using Autofac;
using Autofac.Extensions.DependencyInjection;
using BaseArchitecture.Infrastructures.Configs;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace BaseArchitecture.Worker.Hangfire
{
    class Runner
    {
        public static IConfiguration Configuration { get; private set; }

        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            var connectionString = Configuration.GetValue<string>("connectionString");

            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(containerBuilder =>
                {
                    containerBuilder
                        .RegisterRepository()
                        .RegisterUnitOfWork()
                        .RegisterICommandHandler()
                        .RegisterMessageHandler();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .RegisterMessageDispatcher()
                        .RegisterDbContext(connectionString!)
                        .AddHangfire(configuration => configuration
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseSqlServerStorage(
                            connectionString,
                            new SqlServerStorageOptions
                            {
                                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                QueuePollInterval = TimeSpan.Zero,
                                UseRecommendedIsolationLevel = true,
                                DisableGlobalLocks = true
                            })
                        .UseSerializerSettings(new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            SerializationBinder = new CustomSerializationBinder()
                        }));

                    services.AddHangfireServer();
                });
        }
    }
}