using Engine.Input.Mapper;
using Game.Scripts.Library;
using Game.Maps.Services;
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
}