namespace Scenes.Battle.Interfaces;

/// <summary>
/// The battle targeting view interface, allows checking if a specified sprite is selected.
/// </summary>
public interface IBattleTargetingView {
    /// <summary>
    /// Function that checks if a specified sprite is selected.
    /// </summary>
    /// <param name="spriteIndex">The index in the sprite array.</param>
    /// <returns>If the specified sprite is highlighted.</returns>
    public bool IsSelected(int spriteIndex);
}