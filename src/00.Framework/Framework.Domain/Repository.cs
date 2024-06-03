using Framework.Domain.Entities;

namespace Framework.Domain;

public interface Repository
{
    void RaiseEvent(AggregateRoot entity);
}