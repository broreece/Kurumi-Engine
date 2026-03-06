namespace UI.Input;

using Engine.Input.ControllerInterfaces;

/// <summary>
/// The menu input controller class, contained by menu state.
/// </summary>
public interface IMenuInputController : ISelectable, IVerticalMoveable, ICancelable, IEscapeable {}