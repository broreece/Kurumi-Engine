using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Context.Variables.Exceptions;

public sealed class VariableNotFoundException : EngineException 
{
    public VariableNotFoundException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}