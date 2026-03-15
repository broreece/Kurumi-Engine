namespace UI.Component.Components;

using UI.Component.Core;
using SFML.Graphics;

/// <summary>
/// The list text component class, inherits from the component abstract class.
/// </summary>
public class ListTextComponent : TextComponentBase {
    /// <summary>
    /// Constructor for the list text component.
    /// </summary>
    /// <param name="xPosition">The x position of the text.</param>
    /// <param name="yPosition">The y position of the text.</param>
    /// <param name="fontSize">The font size.</param>
    /// <param name="fontFileName">The font file name.</param>
    /// <param name="text">The 2D array containing each page of text.</param>
    public ListTextComponent(int xPosition, int yPosition, float fontSize, string fontFileName, string text)
        : base(xPosition, yPosition, fontSize, fontFileName) {
        this.text = text;
    }

    /// <summary>
    /// Getter for the pages of text that the basic text window displays.
    /// </summary>
    /// <returns>The array of different text pages.</returns>
    public string GetText() {
        return text;
    }

    /// <summary>
    /// Function that draws the current pages text on the window.
    /// </summary>
    public override Drawable CreateSprite() {
        Text lineText = new() {
            DisplayedString = text,
            Font = font,
            Position = new(xPosition, yPosition),
            CharacterSize = (uint) fontSize
        };
        return lineText;
    }

    private readonly string text;
}