namespace UI.Input;

using Engine.Input.ControllerInterfaces;

/// <summary>
/// The dialogue input controller class, contained by dialogue state.
/// </summary>
public interface IDialogueInputController : ISelectable {}

/// Commenting here to mention although this interface does nothing and might be replaceable just with ISelectable it might break our
/// existing "Every UI state has a linked input controller and we might add functions here later.
