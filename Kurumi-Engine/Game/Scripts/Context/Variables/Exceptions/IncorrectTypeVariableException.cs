using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Context.Variables.Exceptions;

public sealed class IncorrectTypeVariableException : EngineException 
{
    public IncorrectTypeVariableException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}