namespace Utils.Finishable;

/// <summary>
/// A basic interface used for concepts that can finish and allows for low level access to check if it's finished.
/// </summary>
public interface IFinishable
{
    public bool IsFinished();
}