namespace UI.Component.Core;

using SFML.Graphics;

/// <summary>
/// The public text UI component base abstract class.
/// </summary>
public abstract class TextComponentBase : PositionableComponent {
    /// <summary>
    /// Constructor for the text component base, inherited by components that require a font size and file name.
    /// </summary>
    /// <param name="xPosition">The x position of the text.</param>
    /// <param name="yPosition">The y position of the text.</param>
    /// <param name="fontSize">The font size.</param>
    /// <param name="fontFileName">The font file name.</param>
    protected TextComponentBase(int xPosition, int yPosition, float fontSize, string fontFileName) : base(xPosition, yPosition) {
        this.fontSize = fontSize;
        font = new(fontFileName);
    }

    protected readonly Font font;
    protected readonly float fontSize;
}