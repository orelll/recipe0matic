using Domain;
using Domain.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Infrastructure.Commands;

public class InMemoryCommandBus : ICommandBus
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AsyncPolicy _retryPolicy;

    public InMemoryCommandBus(IServiceProvider serviceProvider)
        : this(serviceProvider, null)
    {
    }

    public InMemoryCommandBus(
        IServiceProvider serviceProvider,
        AsyncPolicy? retryPolicy)
    {
        _serviceProvider = serviceProvider;

        _retryPolicy = retryPolicy != null ? retryPolicy! : Policy.NoOpAsync();
    }

    public async Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class
    {
        var wasHandled = await TrySend(command, cancellationToken).ConfigureAwait(true);

        if (!wasHandled)
        {
            throw new InvalidOperationException($"Unable to find handler for command '{command.GetType().Name}'");
        }
    }

    public async Task<bool> TrySend<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class
    {
        using var scope = _serviceProvider.CreateScope();

        var commandHandler = scope.ServiceProvider.GetService<ICommandHandler<TCommand>>();

        if (commandHandler == null)
        {
            return false;
        }

        await _retryPolicy.ExecuteAsync((ct) => commandHandler.Handle(command, ct), cancellationToken)
            .ConfigureAwait(false);

        return true;
    }
}