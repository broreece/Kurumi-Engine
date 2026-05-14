using Engine.Input.Mapper;
using Engine.Systems.Combat.Core;

using Game.Maps.Services;
using Game.Scripts.Library;

using Infrastructure.Persistance.Services;
using Infrastructure.Rendering.Core;

namespace Engine.Context.Containers;

public sealed class GameServices 
{
    public required MapService MapService { get; init; }
    public required SaveService SaveService { get; init; }

    public required ScriptLibrary ScriptLibrary { get; init; }
    
    public required InputMapper InputMapper { get; init; }

    public required RenderSystem RenderSystem { get; init; }

    // Systems.
    public required DamageCalculator DamageCalculator { get; init; }
}