using System.Collections;

namespace Framework.Domain.Events;

public interface IMessageDispatcher
{
    void Publish(IEnumerable message);
}