using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Context.Capabilities.Exceptions;

public sealed class FormationNotFoundException : EngineException 
{
    public FormationNotFoundException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}