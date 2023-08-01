using MassTransit.Mediator;

namespace Application.Dispatchers;

public class MasstransitMediatorCommandDispatcher: ICommandDispatcher
{
    private readonly IMediator _mediator;

    public MasstransitMediatorCommandDispatcher(IMediator mediator) => _mediator = mediator;

    public Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class => _mediator.Send(command, cancellationToken);
}