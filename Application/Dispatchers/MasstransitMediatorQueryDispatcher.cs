using MassTransit.Mediator;

namespace Application.Dispatchers;

public class MasstransitMediatorQueryDispatcher:IQueryDispatcher
{
    private readonly IMediator _mediator;
    
    public MasstransitMediatorQueryDispatcher(IMediator mediator) => _mediator = mediator;
    
    public async Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : class where TResult : class
    {
        var client = _mediator.CreateRequestClient<TQuery>();
        var response = await client.GetResponse<TResult>(query, cancellationToken);

        return response.Message;
    }
}