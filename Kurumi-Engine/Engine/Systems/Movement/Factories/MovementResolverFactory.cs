using Data.Definitions.Maps.Core;
using Data.Runtime.Maps.Core;
using Data.Runtime.Spatials;
using Engine.Systems.Movement.Core;
using Engine.Systems.Navigation.Factories;
using Infrastructure.Database.Base;

namespace Engine.Systems.Movement.Factories;

public sealed class MovementResolverFactory 
{
    private readonly NavigationGridFactory _navigationGridFactory;

    public MovementResolverFactory(Registry<Tile> tileRegistry, IPositionProvider party) 
    {
        _navigationGridFactory = new(tileRegistry, party);
    }

    public MovementResolver Create(Map map) 
    {
        return new MovementResolver(map, _navigationGridFactory.Create(map));
    }
}