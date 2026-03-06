namespace UI.Input;

using Engine.Input.ControllerInterfaces;

/// <summary>
/// The file selector input controller class, contained by file selector state.
/// </summary>
public interface IInventoryInputController : ISelectable, IHorizontalMoveable, IVerticalMoveable, ICancelable, IEscapeable {}