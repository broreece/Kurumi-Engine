using Engine.Input.Base;
using Engine.UI.Elements;

using Utils.Interfaces;

namespace Game.UI.Overlays.Base;

public interface IUIOverlay : IFinishable 
{
    public void Update(float deltaTime);

    public void HandleInput(InputState inputState);

    public UIElement GetUIElement();

    public bool TakesControl();
}