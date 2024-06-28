using Framework.Domain.Entities;

namespace Framework.Domain;

public interface WriteRepository
{
    void RaiseEvent(AggregateRoot entity);
}