using Domain;

namespace WebApi.Features.Recipe.Models;

public class IngredientModelDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }

    public static IngredientModelDto FromDomain(Ingredient ingredient) => new()
    {
        Id = ingredient.Id,
        Name = ingredient.Name
    };
}