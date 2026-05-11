using Data.Runtime.Scripts.Execution;

namespace Data.Runtime.Scripts.Scheduler;

public interface IScriptScheduler
{
    public void AddExecutingScript(ScriptExecution executingScript);
}