namespace Domain;

public class Ingredient
{
    public string Id { get; init; }
    public string Name { get; private set; }

    private Ingredient(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Ingredient Load(string id, string name) => new(id, name);
}