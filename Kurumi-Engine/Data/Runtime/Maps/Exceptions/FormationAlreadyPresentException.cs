using Infrastructure.Exceptions.Base;

namespace Data.Runtime.Maps.Exceptions;

public sealed class FormationAlreadyPresentException : EngineException 
{
    public FormationAlreadyPresentException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}