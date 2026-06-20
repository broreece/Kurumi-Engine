// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Data.Models.Characters.Exceptions;

public sealed class CharacterNotFoundException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public CharacterNotFoundException(string message) : base(message) {}
}