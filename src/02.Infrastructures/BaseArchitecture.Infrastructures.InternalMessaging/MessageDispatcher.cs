using System.Collections;
using System.Reflection;
using Framework.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BaseArchitecture.Infrastructures.InternalMessaging;

public class MessageDispatcher : IMessageDispatcher
{
    readonly IEnumerable<Type> allHandlers;
    readonly string handlerMethodName;

    public MessageDispatcher(IServiceProvider serviceProvider,
        Type messageHandlerType, string handlerMethodName,
        Assembly messageHandlerAssembly)
    {
        if (!messageHandlerType.IsGenericType)
            throw new InvalidOperationException(
                $"{nameof(messageHandlerType)} must be generic");
        if (!messageHandlerType.GetMethods()
                .Any(m => m.Name == handlerMethodName))
            throw new InvalidOperationException(
                $"The message handler type requires at leat one method named `{handlerMethodName}`");

        this.handlerMethodName = handlerMethodName;
        this.serviceProvider = serviceProvider;
        allHandlers =
            messageHandlerAssembly.GetHandlerTypes(messageHandlerType);
    }

    readonly IServiceProvider serviceProvider;

    public void Publish(IEnumerable messages)
    {
        foreach (var handlerType in allHandlers)
        foreach (var message in messages)
        {
            if (handlerType.GetInterfaces().Any(i =>
                    message.GetType()
                        .IsAssignableTo(i.GetGenericArguments().First())))
            {
                //TODO: Optimize this:
                var methods = handlerType.GetMethods().Where(m =>
                    m.Name == handlerMethodName &&
                    m.GetParameters().Length == 1 && m.GetParameters().First()
                        .ParameterType.IsAssignableFrom(message.GetType()));
                foreach (var method in methods)
                {
                    try
                    {
                        var instance = Instanciate(handlerType);
                        method.Invoke(instance, new[] { message });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            $"Failed to accomplish handling the event: {message.GetType().Name}. \n Exception: {ex.Message}");
                    }
                }
            }
        }
    }

    object Instanciate(Type handlerType)
        => ActivatorUtilities.CreateInstance(serviceProvider, handlerType);
}