using Data.Definitions.Actors.Core;

namespace Data.Definitions.Actors.Factories;

public sealed class ActorSpriteFactory 
{
    public ActorSprite Create(int id, int width, int height, string spriteName) 
    {
        return new ActorSprite() {Id = id, SpriteName = spriteName, Width = width, Height = height};
    }
}