// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Infrastructure.Database.Exceptions;

public sealed class IndexKeyNotFoundException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public IndexKeyNotFoundException(string message) : base(message) {}
}