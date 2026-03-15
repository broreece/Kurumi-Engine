namespace States.Map.Input;

using Engine.Input.ControllerInterfaces;

/// <summary>
/// The map input controller class, contained by the map scene input map, used to abstract functions there.
/// </summary>
public interface IMapInputController : ISelectable, IVerticalMoveable, IHorizontalMoveable, IEscapeable {}
