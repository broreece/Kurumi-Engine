namespace Utils.Finishable;

/// <summary>
/// An implementation of the finishable interface that is always finished.
/// </summary>
public sealed class Finished : IFinishable 
{
    public bool IsFinished() => true;
}