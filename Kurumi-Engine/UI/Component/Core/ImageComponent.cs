namespace UI.Component.Core;

using UI.Interfaces;

/// <summary>
/// The public image UI component base abstract class.
/// </summary>
public abstract class ImageComponentBase : PositionableComponent {
    /// <summary>
    /// Constructor for the image component base, inherited by components that require a width and height, along with a string file name.
    /// </summary>
    /// <param name="xPosition">The x position of the component.</param>
    /// <param name="yPosition">The y position of the component.</param>
    /// <param name="width">The width of the component.</param>
    /// <param name="height">The height of the component.</param>
    /// <param name="fileName">The file name of the asset.</param>
    /// <param name="gameWindowDimensionAccessor">The window config object.</param>
    protected ImageComponentBase(int xPosition, int yPosition, int width, int height, int fileWidth, int fileHeight, string fileName,
        IGameWindowDimensionsAccessor gameWindowDimensionAccessor) : base(xPosition, yPosition) {
        // Apply scale:
        int gameWindowWidth = gameWindowDimensionAccessor.GetWidth();
        int gameWindowHeight = gameWindowDimensionAccessor.GetHeight();
        this.width = (gameWindowWidth * (width / 100f)) / fileWidth;
        this.height = (gameWindowHeight * (height / 100f)) / fileHeight;
        this.fileName = fileName;
    }

    /// <summary>
    /// Getter for the component's width.
    /// </summary>
    /// <returns>Returns the component's width.</returns>
    public float GetWidth() {
        return width;
    }

    /// <summary>
    /// Getter for the component's height.
    /// </summary>
    /// <returns>Returns the component's height.</returns>
    public float GetHeight() {
        return height;
    }

    protected readonly float width, height;
    protected readonly string fileName;
}