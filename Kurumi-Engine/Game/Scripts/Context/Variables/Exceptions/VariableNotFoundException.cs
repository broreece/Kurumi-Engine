// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Context.Variables.Exceptions;

public sealed class VariableNotFoundException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public VariableNotFoundException(string message) : base(message) {}
}