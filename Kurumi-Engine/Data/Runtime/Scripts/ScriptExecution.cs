using Game.Scripts.Context.Core;
using Game.Scripts.Core;

namespace Data.Runtime.Scripts;

public sealed class ScriptExecution
{
    private bool _firstStep = true;

    public Script Script { get; }

    public string? CurrentStepKey { get; private set; }

    private bool Finished => CurrentStepKey == null;

    public ScriptExecution(Script script)
    {
        Script = script;
        CurrentStepKey = script.StartingKey;
    } 

    /// <summary>
    /// Updates the currently executing the script taking the script context as a parameter.
    /// </summary>
    /// <param name="scriptContext">The script context required to activate the script.</param>
    public void Update(ScriptContext scriptContext)
    {
        if (!Finished)
        {
            if (!_firstStep)
            {
                CurrentStepKey = Script.NextKey;
            }
            _firstStep = false;
        }

        if (!Finished && !Script.Waiting)
        {
            Script.Activate(scriptContext, CurrentStepKey!);
        }
    }
}