namespace Scripts.Base;

/// <summary>
/// Abstract script class, contains a list of script steps that can be conditionals or nodes.
/// </summary>
public abstract class Script {
    /// <summary>
    /// The constructor for scripts.
    /// </summary>
    protected Script() {
        head = null;
        tail = null;
    }

    /// <summary>
    /// A new script step being added to the script.
    /// </summary>
    /// <param name="newStep">The next script step in the script.</param>
    protected void AddStep(ScriptStep newStep) {
        if (head == null) {
            head = newStep;
        } else if (tail == null) {
            head.SetNext(newStep);
            tail = newStep;
        }
        else {
            tail.SetNext(newStep);
            tail = newStep;
        }
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

    /// <summary>
    /// Getter for the scripts tail step.
    /// </summary>
    /// <returns>The last script step of the script.</returns>
    public ScriptStep ? GetTail() {
        return head;
    }

    /// <summary>
    /// Setter for the scripts tail.
    /// </summary>
    /// <param name="newHead">The new tail of the script.</param>
    public void SetTail(ScriptStep newHead) {
        head = newHead;
    }

    protected ScriptStep ? head, tail;
}