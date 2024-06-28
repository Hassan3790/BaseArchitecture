using System;
using System.Collections;
using Framework.Domain.Events;
using Hangfire;
using Hangfire.States;
using Microsoft.Extensions.DependencyInjection;

namespace BaseArchitecture.Infrastructures.Jobs
{
    public class MessageDispatcher(IServiceProvider serviceProvider) : IMessageDispatcher
    {
        public void Publish(IEnumerable message)
        {
            foreach (var @event in message)
            {
                var eventType = @event.GetType();
                var handlerType = typeof(IHandleMessage<>).MakeGenericType(eventType);
                var handlers = serviceProvider.GetServices(handlerType);

                foreach (var handler in handlers)
                {
                    var eventHandler = handler;
                    BackgroundJob.Enqueue(() => HandleEvent(eventHandler, @event));
                }
            }
        }

        public void HandleEvent(object handler, object @event)
        {
            var method = handler.GetType().GetMethod("Handle");
            method.Invoke(handler, new object[] { @event });
        }
    }
}
