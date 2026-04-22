using Data.Definitions.Actors.Core;
using Data.Definitions.Actors.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Actors;
using Infrastructure.Database.Repositories.Rows.Actors;

namespace Infrastructure.Database.Loaders.Actors;

public sealed class ActorSpriteLoader : IDataLoader<ActorSprite> 
{
    private readonly ActorSpriteRepository _actorSpriteRepository;
    private readonly ActorSpriteFactory _actorSpriteFactory;

    public ActorSpriteLoader(ActorSpriteRepository actorSpriteRepository, ActorSpriteFactory actorSpriteFactory) 
    {
        _actorSpriteRepository = actorSpriteRepository;
        _actorSpriteFactory = actorSpriteFactory;
    }

    public IReadOnlyList<ActorSprite> LoadAll() 
    {
        ActorSpriteRow[] rows = _actorSpriteRepository.LoadAll();
        var actorSprites = new ActorSprite[rows.Length];
        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            actorSprites[index] = _actorSpriteFactory.Create(
                row.Id,
                row.Width,
                row.Height,
                row.SpriteName
            );
        }
        return actorSprites;
    }
}