namespace UI.Component.Components;

using UI.Component.Core;
using SFML.Graphics;
using SFML.System;
using UI.Interfaces;

/// <summary>
/// The choice box class, inherits from the window abstract class.
/// </summary>
public class ChoiceBoxComponent : ImageComponentBase {
    /// <summary>
    /// Choice box constructor.
    /// </summary>
    /// <param name="xPosition">The x position of the choice box.</param>
    /// <param name="yPosition">The y position of the choice box.</param> 
    /// <param name="width">The width of the choice box.</param>
    /// <param name="height">The height of the choice box.</param>
    /// <param name="spacing">The spacing of the choice box.</param>
    /// <param name="choiceBoxFileName">The file name of the choice box.</param>
    /// <param name="choices">The number of all the possible choices.</param>
    /// <param name="choiceBoxFileAccessor">The window config object.</param>
    /// <param name="gameWindowDimensionAccessor">The game window dimension accessor object.</param>
    public ChoiceBoxComponent(int xPosition, int yPosition, int width, int height, int spacing, string choiceBoxFileName, int choices,
        IChoiceBoxFileAccessor choiceBoxFileAccessor, IGameWindowDimensionsAccessor gameWindowDimensionAccessor) 
        : base(xPosition, yPosition, width, height, choiceBoxFileAccessor.GetChoiceSelectionFileWidth(), 
            choiceBoxFileAccessor.GetChoiceSelectionFileHeight(), choiceBoxFileName, gameWindowDimensionAccessor) {
        this.choices = choices;
        this.spacing = spacing;
        currentChoice = 0;

        // Load local config files.
        choiceBoxSelectionFileWidth = choiceBoxFileAccessor.GetChoiceSelectionFileWidth();
        choiceBoxSelectionFileHeight = choiceBoxFileAccessor.GetChoiceSelectionFileHeight();

        // Load texture.
        choiceSelectionTexture = new(fileName);
    }

    /// <summary>
    /// Setter for the choice box's current choice.
    /// </summary>
    /// <param name="newCurrentChoice">The new current choice of the choice box.</param>
    public void SetCurrentChoice(int newCurrentChoice) {
        newCurrentChoice = newCurrentChoice < 0 ? 0 : newCurrentChoice;
        newCurrentChoice = newCurrentChoice > choices - 1 ? choices - 1 : newCurrentChoice;
        currentChoice = newCurrentChoice;
    }

    /// <summary>
    /// Function used to decrement the current choice.
    /// </summary>
    public void DecrementChoice() {
        currentChoice = currentChoice > 0 ? currentChoice - 1 : 0;
    }

    /// <summary>
    /// Function used to increment the current choice.
    /// </summary>
    public void IncrementChoice() {
        currentChoice = currentChoice < choices - 1 ? currentChoice + 1 : choices - 1;
    }

    /// <summary>
    /// Overriden function that draws the choices and the current selection.
    /// </summary>
    public override Drawable CreateSprite() {                
        Vector2f position = new(xPosition, yPosition + (spacing * currentChoice));
        Sprite choiceSelectionSprite = new(choiceSelectionTexture, new IntRect(0, 0,
            choiceBoxSelectionFileWidth, choiceBoxSelectionFileHeight))
        {
            Scale = new Vector2f(width, height),
            Position = position
        };
        return choiceSelectionSprite;
    }

    /// <summary>
    /// Function that returns the choice box components current choice.
    /// </summary>
    /// <returns>The current choice.</returns>
    public int GetChoice() {
        return currentChoice;
    }

    /// <summary>
    /// Getter for the number of choices set in the choice box component.
    /// </summary>
    /// <returns>Thenumber of choices.</returns>
    public int GetNumberOfChoices() {
        return choices;
    }

    /// <summary>
    /// Setter for the number of choices in the choice box component.
    /// </summary>
    /// <param name="newChoices">The new number of choices in the component.</param>
    public void SetChoices(int newChoices) {
        choices = newChoices;
    }
    
    private int currentChoice, choices;
    private readonly int spacing, choiceBoxSelectionFileWidth, choiceBoxSelectionFileHeight;
    private readonly Texture choiceSelectionTexture;
}
