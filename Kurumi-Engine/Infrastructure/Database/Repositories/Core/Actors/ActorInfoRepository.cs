using Infrastructure.Database.Repositories.Base;
using Infrastructure.Database.Repositories.Rows.Actors;
using Infrastructure.Database.Services;

using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Core.Actors;

public sealed class ActorInfoRepository 
{
    private readonly DatabaseService _databaseService;

    public ActorInfoRepository(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public ActorInfoRow[] LoadAll() 
    {
        using SqliteDataReader sqlReader = _databaseService.Query(
            @"SELECT id, behaviour, sprite_id, movement_speed, tracking_range, below_party, passable, see_through, on_touch, auto, on_action, on_find, script_name
                FROM actors"
        );
        var rows = new List<ActorInfoRow>();
        while (sqlReader.Read()) 
        {
            ReaderRow reader = new(sqlReader);
            // Add each row then return the array of all rows.
            rows.Add(new ActorInfoRow() 
            {
                Id = reader.GetInt(Col.Id),
                Behaviour = reader.GetInt(Col.Behaviour),
                SpriteId = reader.GetInt(Col.SpriteId),
                MovementSpeed = reader.GetInt(Col.MovementSpeed),
                TrackingRange = reader.GetInt(Col.TrackingRange),
                BelowParty = reader.GetBool(Col.BelowParty),
                Passable = reader.GetBool(Col.Passable),
                SeeThrough = reader.GetBool(Col.SeeThrough),
                OnTouch = reader.GetBool(Col.OnTouch),
                Auto = reader.GetBool(Col.Auto),
                OnAction = reader.GetBool(Col.OnAction),
                OnFind = reader.GetBool(Col.OnFind),
                ScriptName = reader.GetNullableString(Col.ScriptName),
            });
        }
        return [.. rows];
    }

    private static class Col 
    {
        public const int Id = 0;
        public const int Behaviour = 1;
        public const int SpriteId = 2;
        public const int MovementSpeed = 3;
        public const int TrackingRange = 4;
        public const int BelowParty = 5;
        public const int Passable = 6;
        public const int SeeThrough = 7;
        public const int OnTouch = 8;
        public const int Auto = 9;
        public const int OnAction = 10;
        public const int OnFind = 11;
        public const int ScriptName = 12;
    }
}