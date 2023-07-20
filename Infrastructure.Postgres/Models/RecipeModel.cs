namespace Infrastructure.Postgres.Models;

public class RecipeModel
{
    public string Id { get; set; }
    public string  Name { get; set; }
    public IList<TagModel>? Tags { get; set; }
}