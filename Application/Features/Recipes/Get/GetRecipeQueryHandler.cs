using Domain.Abstractions.Queries;
using Domain.Features;
using OneOf;

namespace Application.Features.Recipes.Get;

public class GetRecipeQueryHandler:IQueryHandler<GetRecipeQuery, OneOf<Recipe, RecipeNotFound, RecipeNotReady>>
{
    private readonly IRecipeRepository _repo;

    public GetRecipeQueryHandler(IRecipeRepository repo)
    {
        _repo = repo;
    }

    public async Task<OneOf<Recipe, RecipeNotFound, RecipeNotReady>> Handle(GetRecipeQuery query, CancellationToken cancellationToken)
    {
        var found = await _repo.Get(query.RecipeId);

        return found != null ? new Recipe(query.RecipeId) : new RecipeNotFound(query.RecipeId);
    }
}

public record Recipe(string Id);
public record RecipeNotFound(string Id);
public record RecipeNotReady(string Id);