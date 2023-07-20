using Domain;
using Domain.Features;

namespace Application.Features.Recipes.Create;

public class CreateRecipeCommandHandler:ICommandHandler<CreateRecipeCommand>
{
    private readonly IRecipeRepository _repo;

    public CreateRecipeCommandHandler(IRecipeRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(CreateRecipeCommand command, CancellationToken cancellationToken = default)
    {
        await _repo.Create(new Recipe
        {
            Id = command.Id,
            Name = command.Name,
            Tags = command.Tags.Select(t => new Tag
            {
                Id = Guid.NewGuid().ToString("n"),
                Value = t
            }).ToList()
        });
    }
}