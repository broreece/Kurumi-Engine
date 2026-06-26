// Data.
using Data.Definitions.Actors.Base;
using Data.Definitions.Actors.Core;
using Data.Definitions.Formations.Core;
using Data.Definitions.Maps.Core;

using Data.Models.Formations.Collections;
using Data.Models.Formations.Exceptions;
using Data.Models.Maps.Collections;
using Data.Models.Maps.Core;

using Data.Runtime.Actors.Core;
using Data.Runtime.Actors.Factories;
using Data.Runtime.Formations.Core;
using Data.Runtime.Formations.Factories;
using Data.Runtime.Maps.Core;
using Data.Runtime.Spatials;

// Engine.
using Engine.Systems.Navigation.Factories;

// Infrastructure.
using Infrastructure.Database.Base;
using Infrastructure.Database.Exceptions;

namespace Data.Runtime.Maps.Factories;

public sealed class MapFactory
{
    // Formation factory.
    private readonly FormationFactory _formationFactory;

    // Formation lookup index for loading formations per map.
    private readonly Index<IReadOnlyList<int>> _mapFormationsIndex;

    // Cached formations models from save file and formation definition registry for building formations.
    private readonly FormationModelCollection _formationModelCollection;
    private readonly Registry<FormationDefinition> _formationDefinitionRegistry;

    // Registries.
    private readonly Registry<ActorInfo> _actorRegistry;
    private readonly Registry<Tile> _tileRegistry;

    // The collection of actor models.
    private readonly ActorModelCollection _actorModelCollection;

    // Actor factories.
    private readonly ActorFactory _actorFactory;
    private readonly DumbTrackingActorFactory _dumbActorFactory;
    private readonly PathedActorFactory _pathedActorFactory;
    private readonly RandomActorFactory _randomActorFactory;
    private readonly SmartTrackingActorFactory _smartActorFactory;

    // The parties position provider.
    private readonly IPositionProvider _partyPosition;

    public MapFactory(
        FormationFactory formationFactory, 
        Index<IReadOnlyList<int>> mapFormationsIndex, 
        FormationModelCollection formationModelCollection, 
        Registry<FormationDefinition> formationDefinitionRegistry, 
        Registry<ActorInfo> actorRegistry, 
        Registry<Tile> tileRegistry, 
        ActorModelCollection actorModelCollection, 
        ActorFactory actorFactory, 
        DumbTrackingActorFactory dumbActorFactory, 
        PathedActorFactory pathedActorFactory, 
        RandomActorFactory randomActorFactory, 
        SmartTrackingActorFactory smartActorFactory, 
        IPositionProvider partyPosition
    )
    {
        _formationFactory = formationFactory;
        _mapFormationsIndex = mapFormationsIndex;
        _formationModelCollection = formationModelCollection;
        _formationDefinitionRegistry = formationDefinitionRegistry;
        _actorRegistry = actorRegistry;
        _tileRegistry = tileRegistry;
        _actorModelCollection = actorModelCollection;
        _actorFactory = actorFactory;
        _dumbActorFactory = dumbActorFactory;
        _pathedActorFactory = pathedActorFactory;
        _randomActorFactory = randomActorFactory;
        _smartActorFactory = smartActorFactory;
        _partyPosition = partyPosition;
    }

    public Map Create(MapModel mapModel)
    {
        // Create tile and actor dictionary by converting our list to a 2D grid in dictionary form.
        IReadOnlyList<TileModel> tiles = mapModel.Tiles;
        var tileDictionary = new Dictionary<(int, int), TileModel>();
        foreach (var tile in tiles)
        {
            tileDictionary.Add((tile.X, tile.Y), tile);
        }

        var map = new Map(mapModel, tileDictionary);

        // After map is created set actors.
        var actorModels = _actorModelCollection.GetActorsOnMap(mapModel.MachineName);

        var actors = new List<Actor>();
        var actorDictionary = new Dictionary<(int, int), List<Actor>>();
        var actorStringDictionary = new Dictionary<string, Actor>();

        var navigationGridFactory = new NavigationGridFactory(_tileRegistry, _partyPosition);
        var navigationGrid = navigationGridFactory.Create(map);

        foreach (var actorModel in actorModels)
        {
            // Create runtime actor using relevant factory.
            ActorInfo actorInfo = _actorRegistry.Get(actorModel.ActorID);
            Actor actor = actorInfo.Behaviour switch
            {
                (int) ActorBehaviour.DumbTracking => _dumbActorFactory.Create(
                    actorInfo,
                    actorModel,
                    _partyPosition
                ),

                (int) ActorBehaviour.FollowsPath => _pathedActorFactory.Create(actorInfo, actorModel),

                (int) ActorBehaviour.RandomMovement => _randomActorFactory.Create(actorInfo, actorModel),

                (int) ActorBehaviour.SmartTracking => _smartActorFactory.Create(
                    actorInfo,
                    actorModel,
                    _partyPosition,
                    navigationGrid
                ),

                _ => _actorFactory.Create(actorInfo, actorModel),
            };

            // Add to universal list.
            actors.Add(actor);

            // Add to actor dictionary.
            if (actorDictionary.ContainsKey((actorModel.XLocation, actorModel.YLocation)))
            {
                actorDictionary[(actorModel.XLocation, actorModel.YLocation)].Add(actor);
            }
            else
            {
                actorDictionary.Add((actorModel.XLocation, actorModel.YLocation), [actor]);
            }

            // Add to actor string dictionary.
            actorStringDictionary.Add(actorModel.ActorKey, actor);
        }
        map.SetActors(actors, actorDictionary, actorStringDictionary);

        // After map is created set enemy formation. 
        var formations = new List<Formation>();
        var formationIdDictionary = new Dictionary<int, Formation>();
        var formationLocationDictionary = new Dictionary<(int, int), Formation>();
        try {
            var mapFormationsIds = _mapFormationsIndex.Get(mapModel.MachineName);
            foreach(var mapFormationId in mapFormationsIds)
            {
                try 
                {
                    var formationModel = _formationModelCollection.Get(mapFormationId);
                    var formation = _formationFactory.Create( 
                        _formationDefinitionRegistry.Get(mapFormationId),
                        formationModel, 
                        navigationGrid
                    );

                    formations.Add(formation);
                    formationLocationDictionary[(formationModel.XLocation, formationModel.YLocation)] = formation;
                    formationIdDictionary[mapFormationId] = formation;
                }
                catch (FormationNotFoundException) {}
            }
        }
        catch (IndexKeyNotFoundException) {}
        map.SetFormations(formations, formationLocationDictionary, formationIdDictionary);

        return map;
    }
}