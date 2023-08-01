using Domain.Features;
using Infrastructure.Postgres.Models;
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
        await _context.Recipes.AddAsync(RecipeModel.ToStorageModel(recipe));
        await _context.SaveChangesAsync();
    }

    public async Task<Recipe?> Get(string id)
    {
        var found = await _context.Recipes
                                            .Include(r => r.Tags)
                                            .Include(r => r.Ingredients)
                                            .FirstOrDefaultAsync(r => r.Id == id);
        return RecipeModel.FromStorageModel(found);
    }

    public async Task<IEnumerable<Recipe>> All()
    {
        var list = await _context.Recipes
                                    .Include(r => r.Tags)
                                    .Include(r => r.Ingredients)
                                    .ToListAsync();
        return list.Select(x => RecipeModel.FromStorageModel(x)!);
    }

    public Task<IEnumerable<Recipe>> List(Func<Recipe, bool> predicate)
    {
        throw new NotImplementedException();
    }
}