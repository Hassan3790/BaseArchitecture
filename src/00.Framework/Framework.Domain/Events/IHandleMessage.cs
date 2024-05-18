namespace Framework.Domain.Events;

public interface IHandleMessage<in TMessage> where TMessage : IDomainEvent
{
    Task Handle(TMessage message);
}