namespace Domain.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : notnull
{
    Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
}
