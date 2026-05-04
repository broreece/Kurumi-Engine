using Game.Scripts.Context.Capabilities.Base;
using Game.UI.Overlays.Core;

namespace Game.Scripts.Context.Capabilities.Interfaces.Universal;

public interface IUIActions : ICapability 
{
    public void OpenBasicTextWindow(IReadOnlyList<string> pages);

    public void OpenGlobalMessage(int timeLimit, string text);

    public ChoiceBoxWithDialogueOverlay OpenTextWindowWithChoice(IReadOnlyList<string> choices, string text);

    public void OpenTextWindowWithNameBox(string text, string name);
}