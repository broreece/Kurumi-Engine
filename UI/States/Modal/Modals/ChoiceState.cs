namespace UI.States.Modal.Modals;

using Engine.Input.Scenes;
using UI.Component.Components;
using UI.Core;
using UI.Input;
using UI.Interfaces;

/// <summary>
/// The choice state class, choice state occurs when in choice boxes or in UIs that enable selecting multiple options.
/// </summary>
public class ChoiceState : UIState, IChoiceInputController {
    /// <summary>
    /// Constructor for the game's choice state.
    /// </summary>
    /// <param name="windowXPosition">The x position of the window.</param>
    /// <param name="windowYPosition">The y position of the window.</param>
    /// <param name="windowWidth">The width of the window.</param>
    /// <param name="windowHeight">The height of the window.</param>
    /// <param name="fontSize">The font size of the choice state.</param>
    /// <param name="fontFileName">The font art file name.</param>
    /// <param name="windowFileName">The window art file name.</param>
    /// <param name="fileAccessors">The window file accessors object.</param>
    /// <param name="gameWindowDimensionsAccessor">The game window dimensions accessor object.</param>
    /// <param name="choiceBoxXPosition">The x position of the choice box.</param>
    /// <param name="choiceBoxYPosition">The y position of the choice box.</param> 
    /// <param name="choiceBoxWidth">The width of the choice box.</param>
    /// <param name="choiceBoxHeight">The height of the choice box.</param>
    /// <param name="spacing">The spacing of the choice box.</param>
    /// <param name="choiceBoxFileName">The choice box file name.</param>
    /// <param name="choices">The array containing all the possible choices.</param>
    public ChoiceState(int windowXPosition, int windowYPosition, int windowWidth, int windowHeight, int fontSize, string fontFileName, 
        string windowFileName, IWindowFileAccessors fileAccessors, IGameWindowDimensionsAccessor gameWindowDimensionsAccessor, 
        int choiceBoxXPosition, int choiceBoxYPosition, int choiceBoxWidth, int choiceBoxHeight, int spacing, string choiceBoxFileName, 
        string[] choices) {
        // Input map.
        inputMap = new ChoiceInputMap(this);

        // Window.
        components.Push(new WindowComponent(windowXPosition, windowYPosition, windowWidth, windowHeight, windowFileName, fileAccessors, 
            gameWindowDimensionsAccessor));
        
        // Choice box.
        choiceBoxComponent = new ChoiceBoxComponent(choiceBoxXPosition, choiceBoxYPosition, choiceBoxWidth, choiceBoxHeight, spacing, 
            choiceBoxFileName, choices.Length, fileAccessors, gameWindowDimensionsAccessor);
        components.Push(choiceBoxComponent);

        // Line text.
        int lineIndex = 0;
        foreach (string choice in choices) {
            components.Push(new ListTextComponent(choiceBoxXPosition, choiceBoxXPosition + (lineIndex * spacing), fontSize, 
                fontFileName, choice));
            lineIndex ++;
        }
    }

    public void Select() {
        // TODO: Implement here.
    }

    /// <summary>
    /// Function used to move the selection up.
    /// </summary>
    public void MoveUp() {
        choiceBoxComponent.DecrementChoice();
    }

    /// <summary>
    /// Function used to move the selection down.
    /// </summary>
    public void MoveDown() {
        choiceBoxComponent.IncrementChoice();
    }

    /// <summary>
    /// Function used to load the choice states current choice.
    /// </summary>
    /// <returns>The choice of the choice box component.</returns>
    public int GetCurrentChoice() {
        return choiceBoxComponent.GetChoice();
    }

    /// <summary>
    /// Function used to close the choice state.
    /// </summary>
    protected override void Close() {
        // TODO: Implement closing animation, lock controls etc whatever is needed here.
        closed = true;
    }

    private readonly ChoiceBoxComponent choiceBoxComponent;
}