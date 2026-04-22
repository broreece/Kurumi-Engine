using Engine.Input.Context.Core;
using Infrastructure.Rendering.Base;

namespace Engine.State.Base;

/// <summary>
/// Contains low level objects such as the game window and input context manager utilized by game states..
/// </summary>
public sealed class StateContext 
{
    public required GameWindow GameWindow { get; init; }
    public required InputContextManager InputContextManager { get; init; }
}