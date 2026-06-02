// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Data.Runtime.Maps.Exceptions;

public sealed class FormationAlreadyPresentException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public FormationAlreadyPresentException(string message) : base(message) {}
}