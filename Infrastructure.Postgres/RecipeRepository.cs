using Domain.Features;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres;

public class RecipeRepository:IRecipeRepository
{
    private readonly RecipesDbContext _context;

    public RecipeRepository(RecipesDbContext context)
    {
        _context = context;
    }

    public async Task Create(Recipe recipe)
    {
        await _context.Recipes.AddAsync(recipe.ToStorageModel());

        await _context.SaveChangesAsync();
    }

    public async Task<Recipe?> Get(string id)
    {
        var found = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
        return found?.FromStorageModel();
    }

    public async Task<IEnumerable<Recipe>> All()
    {
        var list = await _context.Recipes.ToListAsync();
        return list.Select(x => x.FromStorageModel());
    }

    public Task<IEnumerable<Recipe>> List(Func<Recipe, bool> predicate)
    {
        throw new NotImplementedException();
    }
}