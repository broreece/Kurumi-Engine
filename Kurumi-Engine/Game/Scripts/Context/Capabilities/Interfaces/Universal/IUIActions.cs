// Game.
using Game.Scripts.Context.Capabilities.Base;

using Game.UI.Overlays.Core;

// Utility.
using Utils.Finishable;

namespace Game.Scripts.Context.Capabilities.Interfaces.Universal;

public interface IUIActions : ICapability 
{
    public IFinishable OpenBasicTextWindow(IReadOnlyList<string> pages);

    public IFinishable OpenTextWindowWithNameBox(IReadOnlyList<string> pages, string name);

    public void OpenGlobalMessage(int timeLimit, string text);

    public ChoiceBoxWithDialogueOverlay OpenTextWindowWithChoice(IReadOnlyList<string> choices, string text);
}