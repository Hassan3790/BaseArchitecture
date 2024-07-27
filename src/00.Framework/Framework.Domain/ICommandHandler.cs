using ErrorOr;

namespace Framework.Domain;

public interface ICommandHandler<TCommand, TResult>
{
    Task<ErrorOr<TResult>> Handle(TCommand command);
}