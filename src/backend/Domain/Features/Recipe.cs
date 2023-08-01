﻿namespace Domain.Features;

public class Recipe
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public IList<Ingredient>? Ingredients { get; set; }
    public IList<Tag>? Tags { get; set; }
}