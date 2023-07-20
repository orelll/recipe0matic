using Microsoft.Extensions.DependencyInjection;
using Domain;
using Domain.Abstractions.Queries;
using Polly;

namespace Infrastructure.Queries;

public class InMemoryQueryBus : IQueryBus
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AsyncPolicy _retryPolicy;

    public InMemoryQueryBus(IServiceProvider serviceProvider)
        : this(serviceProvider, null)
    {
    }

    public InMemoryQueryBus(
        IServiceProvider serviceProvider,
        AsyncPolicy? retryPolicy)
    {
        _serviceProvider = serviceProvider;

        _retryPolicy = retryPolicy != null ? retryPolicy! : Policy.NoOpAsync();
    }

    public Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : notnull
    {
        var queryHandler =
            _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>()
            ?? throw new InvalidOperationException($"Unable to find handler for Query '{query.GetType().Name}'");

        return _retryPolicy.ExecuteAsync((ct) => queryHandler.Handle(query, ct), cancellationToken);
    }
}