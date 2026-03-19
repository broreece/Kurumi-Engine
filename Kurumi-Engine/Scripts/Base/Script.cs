namespace Scripts.Base;

/// <summary>
/// Abstract script class, contains a list of script steps that can be conditionals or nodes.
/// </summary>
public abstract class Script {
    /// <summary>
    /// The constructor for scripts.
    /// </summary>
    /// <param name="name">The name of the script.</param>
    /// <param name="head">The head of the script.</param>
    protected Script(string name, ScriptStep head) {
        this.name = name;
        this.head = head;
    }

    /// <summary>
    /// Getter for the scripts name.
    /// </summary>
    /// <returns>The name of the script.</returns>
    public string GetName() {
        return name;
    }

    /// <summary>
    /// Getter for the scripts head.
    /// </summary>
    /// <returns>The first script step of the script.</returns>
    public ScriptStep ? GetHead() {
        return head;
    }

    /// <summary>
    /// Setter for the scripts head.
    /// </summary>
    /// <param name="newHead">The new head of the script.</param>
    public void SetHead(ScriptStep newHead) {
        head = newHead;
    }

    protected string name;
    protected ScriptStep ? head;
}