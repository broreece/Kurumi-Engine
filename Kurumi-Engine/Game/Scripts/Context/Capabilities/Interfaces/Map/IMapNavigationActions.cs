using Data.Runtime.Maps.Base;
using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Map;

public interface IMapNavigationActions : ICapability 
{
    public void ChangeMap(MapChangeRequest mapChangeRequest);
}