using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Entities;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Entities;

public sealed class EntityAbilityRepository 
{
    private readonly DatabaseService _databaseService;

    public EntityAbilityRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public EntityAbilityRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT entity_id, ability_id
                FROM entity_abilities"
        );
        var rows = new List<EntityAbilityRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new EntityAbilityRow 
            {
                EntityId = reader.GetInt(Col.EntityId),
                AbilityId = reader.GetInt(Col.AbilityId)
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int EntityId = 0;
        public const int AbilityId = 1;
    }
}