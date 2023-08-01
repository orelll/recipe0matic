namespace Domain.Abstractions.Commands;

public interface ICommandBus
{
    Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class;

    Task<bool> TrySend<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class;
}
