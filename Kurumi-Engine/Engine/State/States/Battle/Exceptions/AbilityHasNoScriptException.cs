using Infrastructure.Exceptions.Base;

namespace Engine.State.States.Battle.Exceptions;

public sealed class AbilityHasNoScriptException : EngineException 
{
    public AbilityHasNoScriptException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}