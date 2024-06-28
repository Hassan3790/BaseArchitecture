using BaseArchitecture.Infrastructures.Jobs;
using Hangfire;
using Hangfire.SqlServer;
using Newtonsoft.Json;

namespace _04.EndPoints.API.Configs
{
    public static class HangfireConfig
    {
        public static IServiceCollection RegisterHangfire(
            this IServiceCollection services,
            string connectionString)
        {
            services
                .AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
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

            return services;
        }

        public static void UseHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
        }
    }
}
