using Infrastructure.Database.Base;

namespace Infrastructure.Database.Factories;

public sealed class NamedDataFactory 
{
    public NamedData Create(int id, string name) 
    {
        return new NamedData{ Id = id, Name = name };
    }
}