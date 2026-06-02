// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Data.Runtime.Formations.Exceptions;

public sealed class FormationException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public FormationException(string message) : base(message) {}
}