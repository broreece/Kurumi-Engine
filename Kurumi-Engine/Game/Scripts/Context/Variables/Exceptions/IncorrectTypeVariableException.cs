// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Context.Variables.Exceptions;

public sealed class IncorrectTypeVariableException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public IncorrectTypeVariableException(string message) : base(message) {}
}