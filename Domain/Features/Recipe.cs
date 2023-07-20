namespace Domain.Features;

public class Recipe
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IList<Tag>? Tags { get; set; }
}