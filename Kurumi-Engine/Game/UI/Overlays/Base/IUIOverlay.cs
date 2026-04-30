using Engine.UI.Elements;

namespace Game.UI.Overlays.Base;

public interface IUIOverlay
{
    public void Update(float deltaTime);

    public UIElement Build();
}