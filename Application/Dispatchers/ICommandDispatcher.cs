namespace Application.Dispatchers;

public interface ICommandDispatcher
{
    Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class;
}