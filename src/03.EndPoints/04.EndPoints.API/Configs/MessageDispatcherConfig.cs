using System.Reflection;
using BaseArchitecture.Infrastructures.InternalMessaging;
using Framework.Domain.Events;

namespace _04.EndPoints.API.Configs;

public static class MessageDispatcherConfig
{
    public static IServiceCollection RegisterMessageDispatcher(
        this IServiceCollection services)
    {
        services.AddSingleton<IMessageDispatcher, MessageDispatcher>(
            s =>
                new MessageDispatcher(
                    s,
                    typeof(IHandleMessage<>),
                    "Handle",
                    Assembly.Load("BaseArchitecture.ApplicationServices")));

        return services;
    }
}