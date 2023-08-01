namespace WebApi.Features.Recipe.Models;

public class RecipeModelDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public IEnumerable<TagModelDto>? Tags { get; set; }
    public IEnumerable<IngredientModelDto>? Ingredients { get; set; }

    public static RecipeModelDto FromDomain(Domain.Features.Recipe recipe) => new()
    {
        Id = recipe.Id,
        Name = recipe.Name,
        Tags = recipe.Tags?.Select(TagModelDto.FromDomain),
        Ingredients = recipe.Ingredients?.Select(IngredientModelDto.FromDomain)
    };
}