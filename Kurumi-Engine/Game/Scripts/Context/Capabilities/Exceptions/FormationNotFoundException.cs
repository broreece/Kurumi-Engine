// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Context.Capabilities.Exceptions;

public sealed class FormationNotFoundException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public FormationNotFoundException(string message) : base(message) {}
}