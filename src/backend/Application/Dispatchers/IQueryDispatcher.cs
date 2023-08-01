namespace Application.Dispatchers;

public interface IQueryDispatcher
{
    Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : class
        where TResult : class;
}