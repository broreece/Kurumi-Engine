using Engine.Input.Base;
using Engine.UI.Elements;

namespace Game.UI.Overlays.Base;

public interface IUIOverlay
{
    public void Update(float deltaTime);

    public void HandleInput(InputState inputState);

    public UIElement GetUIElement();

    public bool IsFinished();

    public bool TakesControl();
}