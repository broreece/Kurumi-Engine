using Engine.Context.Containers;

namespace Engine.Context.Core;

/// <summary>
/// The game context acts as a container for the data, objects and services.
/// </summary>
public sealed class GameContext {
    public required GameData GameData { get; init; }
    public required GameObjects GameObjects { get; init; }
    public required GameServices GameServices { get; init; }
}