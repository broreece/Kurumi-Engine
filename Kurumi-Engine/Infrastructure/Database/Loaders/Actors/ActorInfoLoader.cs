using Data.Definitions.Actors.Core;
using Data.Definitions.Actors.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Actors;
using Infrastructure.Database.Repositories.Rows.Actors;
using Infrastructure.Database.Repositories.Rows.Generic;

namespace Infrastructure.Database.Loaders.Actors;

public sealed class ActorInfoLoader : IDataLoader<ActorInfo> 
{
    private readonly ActorInfoRepository _actorInfoRepository;
    private readonly ActorPathRepository _actorPathRepository;
    private readonly ActorInfoFactory _actorInfoFactory;

    public ActorInfoLoader(ActorInfoRepository actorInfoRepository, 
        ActorPathRepository actorPathRepository,
        ActorInfoFactory actorInfoFactory) 
    {
        _actorInfoRepository = actorInfoRepository;
        _actorPathRepository = actorPathRepository;
        _actorInfoFactory = actorInfoFactory;
    }

    public IReadOnlyList<ActorInfo> LoadAll() 
    {
        ActorInfoRow[] rows = _actorInfoRepository.LoadAll();
        var actorInfo = new ActorInfo[rows.Length];

        // Create a lookup for the actor paths based on the ID of the actor info.
        ILookup<int, ObjectAttributeValueRow> actorPathLookup = _actorPathRepository.LoadAll().ToLookup(
            pathRow => pathRow.ObjectId
        );

        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            var id = row.Id;

            // Load path.
            var path = new List<int>();
            foreach (var pathRow in actorPathLookup[id]) 
            {
                path.Add(pathRow.Value);
            }

            actorInfo[index] = _actorInfoFactory.Create(
                id,
                row.SpriteId,
                row.Behaviour,
                row.MovementSpeed,
                row.TrackingRange,
                row.ScriptName,
                row.BelowParty,
                row.Passable,
                row.SeeThrough,
                row.OnTouch,
                row.Auto,
                row.OnAction,
                row.OnFind,
                path
            );
        }
        return actorInfo;
    }
}