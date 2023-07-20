using Domain.Abstractions.Queries;
using Domain.Features;

namespace Application.Features.Recipes.List;

public class ListRecipesQueryHandler:IQueryHandler<ListRecipesQuery, IEnumerable<Recipe>>
{
    private readonly IRecipeRepository _repo;

    public ListRecipesQueryHandler(IRecipeRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Recipe>> Handle(ListRecipesQuery query, CancellationToken cancellationToken)
    {
        return await _repo.All();
    }
}