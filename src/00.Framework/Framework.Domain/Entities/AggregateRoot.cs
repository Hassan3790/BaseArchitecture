using Framework.Domain.Events;

namespace Framework.Domain.Entities;

public class AggregateRoot
{
    private readonly Queue<DomainEvent> _events = new();
    public IEnumerable<DomainEvent> Events => _events;

    public void ClearEvents()
        => _events.Clear();

    protected void AppendEvent(DomainEvent e)
        => _events.Enqueue(e);
}