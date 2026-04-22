using Engine.UI.Components.Core;
using Engine.UI.Elements;

namespace Game.UI.Views;

public sealed class DialogueView 
{
    private readonly TextComponent _dialogueText;

    public required UIElement Root { get; init; }

    public DialogueView(TextComponent dialogueText) 
    {
        _dialogueText = dialogueText;
    }

    public void SetText(string text) {
        _dialogueText.SetText(text);
    }
}