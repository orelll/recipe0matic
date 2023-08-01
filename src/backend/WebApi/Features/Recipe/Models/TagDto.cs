using Domain;

namespace WebApi.Features.Recipe.Models;

public class TagModelDto
{
    public string Id { get; set; }
    public string Value { get; set; }

    public static TagModelDto FromDomain(Tag tag) => new ()
    {
        Id = tag.Id,
        Value = tag.Value
    };
}