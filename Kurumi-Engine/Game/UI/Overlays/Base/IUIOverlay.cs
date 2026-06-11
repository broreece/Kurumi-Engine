// Engine.
using Engine.Input.Base;

using Engine.UI.Elements;

// Utility.
using Utils.Finishable;

namespace Game.UI.Overlays.Base;

public interface IUIOverlay : IFinishable 
{
    public UIElement UIElement { get; }

    public bool TakesControl { get; }

    public void Update(float deltaTime);

    public void HandleInput(InputState inputState);
}