using Engine.Input.Base;
using Engine.Input.Context.Base;

namespace Engine.Input.Context.Core;

/// <summary>
/// Contains the active context, update function for the active context and allows changing the context or functions
/// to load the context casted as specific interfaces to check actions.
/// </summary>
public sealed class InputContextManager 
{
    private IInputContext? _activeContext;

    public void Update(InputState input) => _activeContext?.Handle(input);

    public void SetContext(IInputContext context) => _activeContext = context;

    public IGameplayInputContext? GetGameplayContext() => _activeContext as IGameplayInputContext;
}