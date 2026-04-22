using SFML.System;

namespace Utils.Maths;

/// <summary>
/// The vector multiplication maths utility class.
/// </summary>
public static class VectorMultiplication 
{
    /// <summary>
    /// The multiple vectors function.
    /// </summary>
    /// <param name="a">A Vector2f object.</param>
    /// <param name="b">A Vector2f object</param>
    /// <returns>A new Vector2f that is the multipled result of two vectors.</returns>
    public static Vector2f Multiple(Vector2f a, Vector2f b) => new(a.X * b.X, a.Y * b.Y);
}