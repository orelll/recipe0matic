namespace Domain.Features;

public interface IRecipeRepository
{
    Task Create(Recipe recipe);
    Task<Recipe?> Get(string id);
    Task<IEnumerable<Recipe>> All();
    Task<IEnumerable<Recipe>> List(Func<Recipe, bool> predicate);
}