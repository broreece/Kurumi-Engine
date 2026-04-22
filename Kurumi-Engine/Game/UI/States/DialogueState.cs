using Game.UI.Views;

namespace Game.UI.States;

public sealed class DialogueState 
{
    private readonly List<string> _pages;
    private readonly DialogueView _dialogueView;

    private int _currentIndex = 0;

    public DialogueState(DialogueView dialogueView, List<string> pages) 
    {
        _dialogueView = dialogueView;
        _pages = pages;
        UpdateUI();
    }

    public void Advance() 
    {
        if (_currentIndex < _pages.Count - 1) {
            _currentIndex ++;
            UpdateUI();
        }
        else {
            // TODO: End here.
        }
    }

    private void UpdateUI() => _dialogueView.SetText(_pages[_currentIndex]);
}