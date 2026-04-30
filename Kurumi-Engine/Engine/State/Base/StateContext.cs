using Engine.Input.Context.Core;

using Game.UI.Overlays.Base;

using Infrastructure.Rendering.Base;

namespace Engine.State.Base;

/// <summary>
/// Contains low level objects relating to the states display.
/// </summary>
public sealed class StateContext 
{
    public required GameWindow GameWindow { get; init; }
    public required InputContextManager InputContextManager { get; init; }

    public Stack<IUIOverlay> UIOverlays { get; } = [];

    public void PushUIOverlay(IUIOverlay newUIOverlay) => UIOverlays.Push(newUIOverlay);

    public void PopUIOverlay() => UIOverlays.Pop();
}