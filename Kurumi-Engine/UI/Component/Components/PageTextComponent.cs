namespace UI.Component.Components;

using UI.Component.Core;
using UI.Core;
using SFML.Graphics;

/// <summary>
/// The text component class, inherits from the component abstract class.
/// </summary>
public class PageTextComponent : TextComponentBase {
    /// <summary>
    /// Constructor for the window with text.
    /// </summary>
    /// <param name="xPosition">The x position of the text.</param>
    /// <param name="yPosition">The y position of the text.</param>
    /// <param name="fontSize">The font size.</param>
    /// <param name="fontFileName">The font file name.</param>
    /// <param name="text">The 2D array containing each page of text.</param>
    public PageTextComponent(int xPosition, int yPosition, float fontSize, string fontFileName, string[,] text)
        : base(xPosition, yPosition, fontSize, fontFileName) {
        this.text = text;
        currentPage = 0;
    }

    /// <summary>
    /// Getter for the maximum number of pages the text component contains.
    /// </summary>
    /// <returns>The maximum number of pages in the text component.</returns>
    public int GetMaxPage() {
        return text.GetLength(0);
    }

    /// <summary>
    /// Geter for the current page of the text component.
    /// </summary>
    /// <returns>The current page of the text component.</returns>
    public int GetCurrentPage() {
        return currentPage;
    }

    /// <summary>
    /// Increments the current page of the page text component.
    /// </summary>
    public void IncrementCurrentPage() {
        currentPage ++;
    }

    /// <summary>
    /// Getter for the pages of text that the basic text window displays.
    /// </summary>
    /// <returns>The array of different text pages.</returns>
    public string[,] GetText() {
        return text;
    }

    /// <summary>
    /// Setter for the new pages of text for a page text component.
    /// </summary>
    /// <param name="newText">The new text value of the page text.</param>
    public void SetText(string[,] newText) {
        text = newText;
    }

    /// <summary>
    /// Function that draws the current pages text on the window.
    /// </summary>
    public override Drawable CreateSprite() {
        TextGroup textGroup = new();
        for (int currentLine = 0; currentLine < text.GetLength(1); currentLine++) {
            // Calculate the text's Y position here.
            Text lineText = new() {
                DisplayedString = text[currentPage, currentLine],
                Font = font,
                Position = new(xPosition, yPosition + (fontSize * currentLine)),
                CharacterSize = (uint) fontSize
            };
            textGroup.Add(lineText);
        }
        return textGroup;
    }

    private int currentPage;
    private string[,] text;
}