using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Universal;

public interface IUIActions : ICapability 
{
    public void OpenBasicTextWindow(string text);

    public void OpenGlobalMessage(int timeLimit, string text);

    public void OpenTextWindowWithChoice(string text, string[] choices);

    public void OpenTextWindowWithNameBox(string text, string name);
}