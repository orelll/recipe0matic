using Domain;
using Domain.Features;

namespace Application.Features.Recipes.Create;

public class CreateRecipeCommandHandler:ICommandHandler<CreateRecipeCommand>
{
    private readonly IRecipeRepository _repo;
    private readonly IIdGenerator _idGenerator;

    public CreateRecipeCommandHandler(IRecipeRepository repo, IIdGenerator idGenerator)
    {
        _repo = repo;
        _idGenerator = idGenerator;
    }

    public async Task Handle(CreateRecipeCommand command, CancellationToken cancellationToken = default)
    {
        await _repo.Create(new Recipe
        {
            Id = command.Id,
            Name = command.Name,
            Tags = command.Tags?.Select(t => new Tag
            {
                Id = _idGenerator.Generate(),
                Value = t
            }).ToList(),
            Ingredients = command.Ingredients?.Select(x => Ingredient.Load(_idGenerator.Generate(), x))
                .ToList()
        });
    }
}