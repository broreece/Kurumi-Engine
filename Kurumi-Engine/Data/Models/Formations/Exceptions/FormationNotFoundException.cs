// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Data.Models.Formations.Exceptions;

public sealed class FormationNotFoundException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public FormationNotFoundException(string message) : base(message) {}
}