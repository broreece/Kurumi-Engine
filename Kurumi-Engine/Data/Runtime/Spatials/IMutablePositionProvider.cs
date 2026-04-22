namespace Data.Runtime.Spatials;

/// <summary>
/// Allows access and mutation of an objects X and Y location.
/// </summary>
public interface IMutablePositionProvider : IPositionProvider 
{
    public new int XLocation { get; set; }

    public new int YLocation { get; set; }

    public int LastX { set; }

    public int LastY { set; }

    public int Facing { set; }

    public void StartMovement();
}