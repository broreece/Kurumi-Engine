namespace Config.Runtime.Map;

/// <summary>
/// The config class for the animated tilesheet config.
/// </summary>
public sealed class AnimatedTileSheetConfig {
    /// <summary>
    /// The constructor for the animated tilesheet config.
    /// </summary>
    /// <param name="animationInterval">The animation interval of animated tiles.</param>
    /// <param name="animatedTileFrames">The number of frames each animated tile has.</param>
    public AnimatedTileSheetConfig(int animationInterval, int animatedTileFrames) {
        this.animationInterval = animationInterval;
        this.animatedTileFrames = animatedTileFrames;
    }

    /// <summary>
    /// Getter for the animation interval config.
    /// </summary>
    /// <returns>The animation interval for animated tile sheets.</returns>
    public int GetAnimationInterval() {
        return animationInterval;
    }

    /// <summary>
    /// Getter for the number of animated frames of tiles config.
    /// </summary>
    /// <returns>The number of animated frames for animated tile sheets.</returns>
    public int GetAnimatedFrames() {
        return animatedTileFrames;
    }

    private readonly int animationInterval, animatedTileFrames;
}