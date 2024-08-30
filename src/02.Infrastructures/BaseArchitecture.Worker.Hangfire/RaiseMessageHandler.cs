using System.Reflection;
using BaseArchitecture.Domain;
using BaseArchitecture.Domain.Employees.Events;
using BaseArchitecture.Persistence.EF;
using Framework.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BaseArchitecture.Worker.Hangfire;

public class RaiseMessageHandler(
    IMessageDispatcher messageDispatcher,
    ApplicationWriteDbContext context)
{
    public async Task Handle()
    {
        var messages = await context
            .Set<OutboxMessage>()
            .Where(q => q.PublishedTime == null)
            .ToListAsync();

        foreach (var message in messages)
        {
            var type = FindType(message.Type);
            var objectMessage = ConvertStringToObject(message.Content, type);

            messageDispatcher.Publish(new List<IDomainEvent> { objectMessage });
            message.Publish();
        }

        await context.SaveChangesAsync();
    }

    private IDomainEvent ConvertStringToObject(string message, Type type)
    {
        var result = JsonConvert.DeserializeObject(message, type);

        if (result is not IDomainEvent)
            throw new InvalidCastException($"can not convert {message} to {type}");

        return (IDomainEvent)result;
    }

    private Type FindType(string typeName)
    {
        var assembly = typeof(EmployeeCreated).Assembly;
        var type = assembly.GetType(typeName);

        if (type is null)
        {
            throw new InvalidOperationException($"can not find {type} in assembly {assembly}");
        }

        return type!;
    }
}