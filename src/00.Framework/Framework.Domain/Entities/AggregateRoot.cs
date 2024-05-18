using Framework.Domain.Events;

namespace Framework.Domain.Entities;

public class AggregateRoot
{
    private readonly Queue<IDomainEvent> _events = new();
    public IEnumerable<IDomainEvent> Events => _events;

    public void ClearEvents()
        => _events.Clear();

    protected void AppendEvent(IDomainEvent e)
        => _events.Enqueue(e);
}