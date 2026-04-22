using Data.Definitions.Maps.Core;
using Data.Runtime.Maps.Core;
using Data.Runtime.Spatials;
using Engine.Systems.Navigation.Core;
using Infrastructure.Database.Base;

namespace Engine.Systems.Navigation.Factories;

public sealed class NavigationGridFactory 
{
    private readonly Registry<Tile> _tileRegistry;
    private readonly IPositionProvider _partyPosition;

    public NavigationGridFactory(Registry<Tile> tileRegistry, IPositionProvider partyPosition) 
    {
        _tileRegistry = tileRegistry;
        _partyPosition = partyPosition;
    }
        
    public NavigationGrid Create(Map map) 
    {
        return new NavigationGrid(map, _tileRegistry, _partyPosition);
    }
}