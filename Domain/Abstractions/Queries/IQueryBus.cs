namespace Domain.Abstractions.Queries;


public interface IQueryBus
{
    Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : notnull;
}