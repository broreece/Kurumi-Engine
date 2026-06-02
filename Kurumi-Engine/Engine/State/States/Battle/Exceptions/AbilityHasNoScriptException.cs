// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Engine.State.States.Battle.Exceptions;

public sealed class AbilityHasNoScriptException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public AbilityHasNoScriptException(string message) : base(message) {}
}