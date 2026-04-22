using Engine.Systems.Navigation.Core;
using Engine.Systems.Perception.Core;

namespace Engine.Systems.Perception.Factories;

public sealed class VisionResolverFactory 
{
    private readonly NavigationGrid _navigationGrid;

    public VisionResolverFactory(NavigationGrid navigationGrid) 
    {
        _navigationGrid = navigationGrid;
    }

    public VisionResolver Create() 
    {
        return new VisionResolver(_navigationGrid);
    }
}