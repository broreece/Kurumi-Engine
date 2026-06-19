// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Infrastructure.Database.Exceptions;

public sealed class RegistryIDNotFoundException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public RegistryIDNotFoundException(string message) : base(message) {}
}