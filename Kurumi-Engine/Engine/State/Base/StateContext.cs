// Data.
using Data.Runtime.Scripts.Execution;
using Data.Runtime.Scripts.Scheduler;

// Engine.
using Engine.Input.Context.Core;

// Game.
using Game.UI.Overlays.Base;

// Infrastructure.
using Infrastructure.Rendering.Base;

namespace Engine.State.Base;

/// <summary>
/// Contains low level objects relating to the states display.
/// </summary>
public sealed class StateContext : IScriptScheduler 
{
    public required GameWindow GameWindow { get; init; }
    public required InputContextManager InputContextManager { get; init; }

    public IList<ScriptExecution> Scripts { get; } = [];

    public Stack<IUIOverlay> UIOverlays { get; } = [];

    public void AddExecutingScript(ScriptExecution executingScript) => Scripts.Add(executingScript);

    public void RemoveExecutingScript(ScriptExecution finishedScript) => Scripts.Remove(finishedScript);

    public void PushUIOverlay(IUIOverlay newUIOverlay) => UIOverlays.Push(newUIOverlay);

    public void PopUIOverlay() => UIOverlays.Pop();
}