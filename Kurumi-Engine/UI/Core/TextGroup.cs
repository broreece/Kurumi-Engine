namespace UI.Core;

using SFML.Graphics;

/// <summary>
/// The public text group class, a type of SFML drawable that contains multiple SFML text objects, used in components to draw
/// multiple lines of text.
/// </summary>
public class TextGroup : Drawable {
    /// <summary>
    /// Constructor for the text group class.
    /// </summary>
    public TextGroup() {
        text = [];
    }

    /// <summary>
    /// Interfaced drawable function that draws the object onto the target.
    /// </summary>
    /// <param name="target">The render target object.</param>
    /// <param name="states">The render states object.</param>
    public void Draw(RenderTarget target, RenderStates states) {
        foreach (Text textLine in text) {
            target.Draw(textLine);
        }
    }

    /// <summary>
    /// Function used to add the line text to the text group.
    /// </summary>
    /// <param name="lineText">The line text to be added.</param>
    public void Add(Text lineText) {
        text.Add(lineText);
    }

    private readonly List<Text> text;
}