using Framework.Domain.Entities;

namespace Framework.Domain.Events
{
    public interface IOutboxManagement
    {
        void Add(AggregateRoot aggregateRoot);
    }
}
