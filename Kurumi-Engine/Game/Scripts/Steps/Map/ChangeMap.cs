using Data.Runtime.Maps.Base;
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Map;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Map;

public sealed class ChangeMap : ScriptStep 
{
    private readonly MapChangeRequest _mapChangeRequest;

    public ChangeMap(string mapName, int xLocation, int yLocation) : base() 
    {
        _mapChangeRequest = new MapChangeRequest{MapName = mapName, X = xLocation, Y = yLocation};
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IMapNavigationActions mapNavigationActions = scriptContext.GetCapability<IMapNavigationActions>();
        mapNavigationActions.ChangeMap(_mapChangeRequest);
    }
}