using Domain;

namespace Infrastructure.Postgres.Models;

public class IngredientModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public static Ingredient FromStorageModel(IngredientModel ingredient) => Ingredient.Load(ingredient.Id, ingredient.Name);
    public static IngredientModel ToStorageModel(Ingredient ingredient) => new()
    {
        Id = ingredient.Id,
        Name = ingredient.Name
    };
}