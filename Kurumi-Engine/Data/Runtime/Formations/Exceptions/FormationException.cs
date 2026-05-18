using Infrastructure.Exceptions.Base;

namespace Data.Runtime.Formations.Exceptions;

public sealed class FormationException : EngineException 
{
    public FormationException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}