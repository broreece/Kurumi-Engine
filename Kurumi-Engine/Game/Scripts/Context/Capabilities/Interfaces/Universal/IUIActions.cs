using Game.Scripts.Context.Capabilities.Base;
using Game.UI.Overlays.Core;

using Utils.Finishable;

namespace Game.Scripts.Context.Capabilities.Interfaces.Universal;

public interface IUIActions : ICapability 
{
    public IFinishable OpenBasicTextWindow(IReadOnlyList<string> pages);

    public void OpenGlobalMessage(int timeLimit, string text);

    public ChoiceBoxWithDialogueOverlay OpenTextWindowWithChoice(IReadOnlyList<string> choices, string text);

    public void OpenTextWindowWithNameBox(string text, string name);
}