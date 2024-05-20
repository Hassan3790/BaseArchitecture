namespace Framework.Domain;

public interface ICommandHandler<TCommand>
{
    Task Handle(TCommand command);
}