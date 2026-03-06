namespace UI.States.Menu.Core;

/// <summary>
/// Menu option public class, contains a menu command enum and a label.
/// </summary>
public class MenuOption {
    /// <summary>
    /// Constructor for the menu option, used in the menu state to handle label and menu command.
    /// </summary>
    /// <param name="label">The string label of the related menu command.</param>
    /// <param name="menuCommand">The menu command enum.</param>
    public MenuOption(string label, MenuCommand menuCommand) {
        this.label = label;
        this.menuCommand = menuCommand;
    }

    /// <summary>
    /// Getter for the menu option's label.
    /// </summary>
    /// <returns>The label of the menu option.</returns>
    public string GetLabel() {
        return label;
    }

    /// <summary>
    /// Getter for the menu option's menu command.
    /// </summary>
    /// <returns>The menu command of the menu option.</returns>
    public MenuCommand GetMenuCommand() {
        return menuCommand;
    }

    private readonly string label;
    private readonly MenuCommand menuCommand;
}