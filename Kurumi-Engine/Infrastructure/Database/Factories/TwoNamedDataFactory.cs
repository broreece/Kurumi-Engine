using Infrastructure.Database.Base;

namespace Infrastructure.Database.Factories;

public sealed class TwoNamedDataFactory
{
    public TwoNamedData Create(int id, string firstName, string secondName)
    {
        return new TwoNamedData { Id = id, FirstName = firstName, SecondName = secondName };
    }
}