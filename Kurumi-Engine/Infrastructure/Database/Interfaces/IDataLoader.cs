namespace Infrastructure.Database.Interfaces;

/// <summary>
/// Data loader interface allows loading of a read only list of a set type.
/// </summary>
/// <typeparam name="T">The type of data stored in the read only list.</typeparam>
public interface IDataLoader<T> 
{
    public IReadOnlyList<T> LoadAll();
}