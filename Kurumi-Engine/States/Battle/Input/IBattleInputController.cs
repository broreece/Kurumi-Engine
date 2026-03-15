namespace States.Battle.Input;

using Engine.Input.ControllerInterfaces;

/// <summary>
/// The battle input controller class, contained by the battle scene input map, used to link inputs to functions there.
/// </summary>
public interface IBattleInputController : ISelectable, IVerticalMoveable, IHorizontalMoveable, ICancelable {}
