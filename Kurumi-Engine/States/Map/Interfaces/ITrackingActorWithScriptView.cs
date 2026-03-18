namespace States.Map.Interfaces;

/// <summary>
/// The public tracking actor with script view interface. Used when checking tracked actors on the map state and needed to access the map script.
/// </summary>
public interface ITrackingActorWithScriptView : ITrackingActorView, IScriptAccessor {
}