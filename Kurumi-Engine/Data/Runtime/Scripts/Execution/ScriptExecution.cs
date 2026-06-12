// Data.
using Data.Runtime.Scripts.Scheduler;

// Game.
using Game.Scripts.Context.Core;
using Game.Scripts.Core;

namespace Data.Runtime.Scripts.Execution;

public sealed class ScriptExecution
{
    private bool _firstStep = true;

    public Script Script { get; }

    public string? CurrentStepKey { get; private set; }

    public bool Finished => CurrentStepKey == null && !Script.PotentialNextKeyExists();

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
                CurrentStepKey = Script.GetNextKey();
            }
            _firstStep = false;
        }

        if (!Finished && !Script.IsWaiting())
        {
            Script.Activate(scriptContext, CurrentStepKey!);
        }
    }

    public void RunToPauseOrFinish(ScriptContext scriptContext, IScriptScheduler scriptScheduler)
    {
        while (!Finished && !Script.IsWaiting())
        {
            Script.Activate(scriptContext, CurrentStepKey!);
            CurrentStepKey = Script.GetNextKey();
        }
        if (!Finished && Script.IsWaiting())
        {
            scriptScheduler.AddExecutingScript(this);
        }
    }
}