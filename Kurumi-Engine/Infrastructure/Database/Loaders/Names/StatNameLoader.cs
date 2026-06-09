// Infrastructure.
using Infrastructure.Database.Base;
using Infrastructure.Database.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Names;
using Infrastructure.Database.Repositories.Rows.Entities;

namespace Infrastructure.Database.Loaders.Names;

public sealed class StatNameLoader : IDataLoader<TwoNamedData> 
{
    private readonly StatNameRepository _statNameRepository;
    private readonly TwoNamedDataFactory _statNameFactory;

    // Index.
    private readonly Dictionary<string, int> _statShortNamesIndex = [];

    public StatNameLoader(StatNameRepository statNameRepository, TwoNamedDataFactory statNameFactory) 
    {
        _statNameRepository = statNameRepository;
        _statNameFactory = statNameFactory;
    }

    public IReadOnlyList<TwoNamedData> LoadAll() 
    {
        StatRow[] rows = _statNameRepository.LoadAll();
        var statNames = new TwoNamedData[rows.Length];
        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            var id = row.Id;
            var shortName = row.ShortName;

            statNames[index] = _statNameFactory.Create(
                id, 
                row.Name, 
                shortName
            );

            // Update index.
            _statShortNamesIndex[shortName.ToLower()] = id;
        }
        return statNames;
    }

    /// <summary>
    /// Loads and returns an index lookup dictionary for stat short names.
    /// </summary>
    /// <returns>A dictionary where the stat short name is the key and the ID is the value.</returns>
    public IReadOnlyDictionary<string, int> LoadStatShortNameIndex() {
        return _statShortNamesIndex;
    }
}