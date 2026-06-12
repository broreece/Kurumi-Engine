// Data.
using Data.Definitions.Actors.Base;
using Data.Definitions.Maps.Base;

using Data.Runtime.Actors.Core;
using Data.Runtime.Formations.Base;
using Data.Runtime.Formations.Core;
using Data.Runtime.Maps.Base.Change;
using Data.Runtime.Maps.Core;
using Data.Runtime.Parties.Core;
using Data.Runtime.Scripts.Execution;

// Engine.
using Engine.Context.Containers;
using Engine.Context.Core;

using Engine.Input.Context.Contexts;

using Engine.State.Base;
using Engine.State.States.Maps.Base;
using Engine.Systems.Animation.Map.Core;
using Engine.Systems.Camera;
using Engine.Systems.Movement.Core;
using Engine.Systems.Perception.Core;
using Engine.Systems.Perception.Factories;
using Engine.Systems.Rendering.Core;

// Game.
using Game.Scripts.Context.Builder.Factories;
using Game.Scripts.Context.Core;

namespace Engine.State.States.Maps.Core;

/// <summary>
/// Renders maps, actors and the party. Contains input handler and systems relating to movement for both parties,
/// actors and animations.
/// </summary>
public sealed class MapState : IGameState 
{
    // Context.
    private readonly GameContext _gameContext;
    private readonly StateContext _stateContext;

    // Party.
    private readonly Party _party;

    // Starting script.
    private readonly string? _startingScript;

    // Context builder.
    private readonly MapScriptContextBuilderFactory _mapScriptContextBuilderFactory;

    // Cached objects.
    private GameObjects? _gameObjects;
    private GameData? _gameData;
    private GameServices? _gameServices;

    // Map, movement resolver and vision resolver.
    private Map? _currentMap;
    private MovementResolver? _movementResolver;
    private VisionResolver? _visionResolver;

    // Cached variables.
    private int _tileWidth;
    private int _tileHeight;

    // Renderers.
    private MapRenderer? _mapRenderer;
    private ActorRenderer? _actorRenderer;
    private PartyMapRenderer? _partyMapRenderer;

    // Camera.
    private Camera? _camera;

    // Animation managers.
    private WalkAnimationManager? _walkAnimationManager;
    private MapAnimationManager? _mapAnimationManager;

    // Script context.
    private ScriptContext? _mapScriptContext;

    public ScriptContext ScriptContext => _mapScriptContext!;

    internal MapState(
        GameContext gameContext, 
        StateContext stateContext, 
        MapScriptContextBuilderFactory mapScriptContextBuilderFactory, 
        string? startingScript
    ) 
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
        _party = gameContext.GameObjects.Party;
        _mapScriptContextBuilderFactory = mapScriptContextBuilderFactory;
        _startingScript = startingScript;
    }

    public void OnEnter() 
    {
        // Break up functionality for readability.
        CacheDependencies();
        InitializeMap();
        InitializeInput();
        InitializeCamera();

        // Execute any starting scripts.
        ExecuteStartingScripts();

        // Check if in range of actors or formations.
        ExecuteAllOnFindScripts();
        CheckInRangeFormations();
    }

    public void OnExit() {}

    public void Update(float deltaTime) 
    {
        // Check if game is paused.
        if (!_stateContext.IsPaused()) {
            // Handle on touch and on find scripts.
            var isCurrentlyMoving = _party.MovementProgress < 1f;
            var movementJustFinished = !isCurrentlyMoving && _party.MovingLastFrame;

            HandleInteractions();

            if (movementJustFinished) 
            {
                HandlePartyMovementFinished();
            }

            MoveAllActors(deltaTime);
            UpdateAllFormations(deltaTime);

            // Update animations.
            _walkAnimationManager!.Update(deltaTime);
            _mapAnimationManager!.Update(deltaTime);
        }

        UpdateCamera();

        UpdateRenderers();

        if (_gameObjects!.MapChangeRequest != null) 
        {
            HandleMapChangeRequest(_gameObjects.MapChangeRequest);
            _gameObjects.MapChangeRequest = null;
        }
    }

    private void CacheDependencies() 
    {
        // Engine objects.
        _gameObjects = _gameContext.GameObjects;
        _gameData = _gameContext.GameData;
        _gameServices = _gameContext.GameServices;

        _currentMap = _gameObjects.CurrentMap;

        // Variables.
        var tileConfig = _gameData.ConfigProvider.TileSheetConfig;
        _tileWidth = tileConfig.Width;
        _tileHeight = tileConfig.Height;

        // Map script context.
        var mapScriptContextBuilder = _mapScriptContextBuilderFactory.Create();
        _mapScriptContext = mapScriptContextBuilder.BuildScriptContext();
    }

    private void InitializeMap() 
    {
        // Map renderers.
        // Cast the list of actors and formations to the map entity shared interface.
        IEnumerable<IMapEntity> combined = _currentMap!.Actors!.Cast<IMapEntity>().Concat(
            _currentMap.Formations!.Cast<IMapEntity>()
        );
        _actorRenderer = _gameServices!.ActorRendererFactory!.Create([.. combined]);
        _walkAnimationManager = _gameServices.WalkAnimationManagerFactory!.Create([.. combined], _party);
        _mapAnimationManager = _gameServices.MapAnimationManagerFactory.Create();
        _mapRenderer = _gameServices.MapRendererFactory.Create(
            _currentMap.Tiles, 
            _currentMap.TileSheetName, 
            _currentMap.AnimatedTileSheetName, 
            _currentMap.MapBackgroundArtName
        );
        _walkAnimationManager = _gameServices.WalkAnimationManagerFactory.Create([.. combined], _party);

        // Party renderer.
        _partyMapRenderer = _gameServices!.PartyMapRendererFactory.Create(_party);

        // Navigation grid related objects.
        var navigationGrid = _gameServices.NavigationGridFactory.Create(_currentMap);
        var visionResolverFactory = new VisionResolverFactory(navigationGrid);
        _visionResolver = visionResolverFactory.Create();
    }

    private void InitializeInput() 
    {
        _movementResolver = _gameServices!.MovementResolverFactory.Create(_gameObjects!.CurrentMap);
        _stateContext.InputContextManager.SetContext(new MapInputContext(_party, _movementResolver));
    }

    private void InitializeCamera() 
    {
        var displayConfig = _gameData!.ConfigProvider.DisplayConfig;

        _camera = new Camera(displayConfig.ViewWidth, displayConfig.ViewHeight);
        _stateContext.GameWindow.SetView(_camera.View);
    }

    private void HandleInteractions()
    {
        if (_stateContext.InputContextManager.GetGameplayContext()!.InteractRequested) 
        {
            _stateContext.InputContextManager.GetGameplayContext()!.InteractRequested = false;
            
            // Get location of party.
            var partyX = _party.XLocation;
            var partyY = _party.YLocation;
            var facing = _party.SpriteState;

            // Calculate the location being interacted with.
            var xChange = facing == (int) SpriteState.West ? -1 : facing == (int) SpriteState.East ? 1 : 0;
            var yChange = facing == (int) SpriteState.South ? 1 : facing == (int) SpriteState.North ? -1 : 0;
            var targetX = partyX + xChange;
            var targetY = partyY + yChange;
            var newFacing = facing == 
                (int) SpriteState.North ? (int) SpriteState.South 
                : facing == (int) SpriteState.East ? (int) SpriteState.West 
                : facing == (int) SpriteState.South ? (int) SpriteState.North 
                : (int) SpriteState.East;

            // Set new facing direction if the actor turns and activate script.
            var actors = _currentMap!.GetActorsAt(targetX, targetY);

            foreach (var actor in actors) 
            {
                if ((int) ActorBehaviour.StationaryDoesNotTurn != actor.Behaviour) 
                {
                    actor.SpriteState = newFacing;
                }

                // Load potential script and activate.
                if (actor.OnAction && actor.Script != null) 
                {
                    var executingScript = new ScriptExecution(actor.Script);
                    executingScript.RunToPauseOrFinish(_mapScriptContext!, _stateContext);
                }
            }
        }
    }

    private void HandlePartyMovementFinished()
    {
        ExecuteOnTouchScripts();

        CheckAnyFormationBesideParty();

        ExecuteAllOnFindScripts();
        CheckInRangeFormations();

        _party.MovingLastFrame = false;
    }

    private void ExecuteOnTouchScripts()
    {
        var actors = _currentMap!.GetActorsAt(_party.XLocation, _party.YLocation);
        foreach (var actor in actors) 
        {
            if (actor.OnTouch && actor.Script != null) 
            {
                var executingScript = new ScriptExecution(actor.Script);
                executingScript.RunToPauseOrFinish(_mapScriptContext!, _stateContext);
            }
        }
    }

    private void UpdateCamera()
    {
        float interpolatedX = _party.LastX + (_party.PartyModel.XLocation - _party.LastX) * _party.MovementProgress;
        float interpolatedY = _party.LastY + (_party.PartyModel.YLocation - _party.LastY) * _party.MovementProgress;

        float worldX = interpolatedX * _tileWidth + _tileWidth / 2f;
        float worldY = interpolatedY * _tileHeight + _tileHeight / 2f;

        _camera!.Follow(worldX, worldY, _currentMap!.Width * _tileWidth, _currentMap.Height * _tileHeight);
        _stateContext.GameWindow.SetView(_camera.View);
    }

    private void UpdateRenderers()
    {
        _mapRenderer!.Update(_mapAnimationManager!, _camera!.View);
        _actorRenderer!.Update(_camera.View);
        _partyMapRenderer!.Update(_camera.View);
    }

    private void MoveAllActors(float deltaTime) {
        if (_currentMap!.Actors != null)
        {
            // Move all actors.
            foreach (var actor in _currentMap!.Actors!) 
            {
                var controllers = actor.Controllers;
                if (controllers.Count > 0) 
                {
                    var currentController = controllers.Peek();
                    // Pop controller if it's finished, otherwise continue updating.
                    if (currentController.IsFinished()) 
                    {
                        actor.PopController();
                        actor.MaintainFacing = false;
                    }
                    else 
                    {
                        currentController.Update(deltaTime);
                        if (currentController.CanMove && !actor.IsMoving) 
                        {
                            // Execute move.
                            var move = currentController.GetMove(actor);
                            if (move >= 0) 
                            {
                                _movementResolver!.TryMove(actor, move);
                                currentController.ExecuteMove();

                                // Execute on find script.
                                ExecuteOnFindScript(actor);

                                // After actor moves visiblity might change.
                                CheckInRangeFormations();
                            }
                        }
                    }
                }
            }
        }
    }

    private void UpdateAllFormations(float deltaTime)
    {
        if (_currentMap!.Formations != null)
        {
            // Move all formations.
            foreach (var formation in _currentMap.Formations)
            {
                if (!formation.Dead) {
                    UpdateFormation(formation, deltaTime);
                }
            }
        }
    }

    private void UpdateFormation(Formation formation, float deltaTime) 
    {
        var controller = formation.GetCurrentController();
        if (controller != null)
        {
            formation.Update(deltaTime);
            controller.Update(deltaTime);
            if (controller.CanMove && !formation.IsMoving)
            {
                CheckFormationBesideParty(formation);

                // Execute move.
                var move = controller.GetMove(formation);
                if (move >= 0)
                {
                    // If the formation can execute moves we reset their alert status.
                    formation.ResetAlertTimer();

                    _movementResolver!.TryMove(formation, move);
                    controller.ExecuteMove();

                    // Check if the party is in range of the formation.
                    CheckInRangeFormation(formation);

                    // After formation moves visiblity might change.
                    ExecuteAllOnFindScripts();
                }
                // If the formation can not execute any moves increment their alert counter.
                else
                {
                    if (formation.AlertLimitReached)
                    {
                        formation.Alert = false;
                        formation.ResetAlertTimer();
                    }
                }
            }
        }
    }

    private void ExecuteStartingScripts()
    {
        if (_startingScript != null)
        {
            var executingScript = new ScriptExecution(
                _gameContext.GameServices.ScriptLibrary.GetMapScript(_startingScript)
            );
            executingScript.RunToPauseOrFinish(_mapScriptContext!, _stateContext);
        }
    }

    private void CheckInRangeFormations()
    {
        foreach (var formation in _currentMap!.Formations!)
        {
            CheckInRangeFormation(formation);
        }
    }

    private void CheckInRangeFormation(Formation formation) 
    {
        if (_visionResolver!.CanSee(formation, _party, formation.TrackingRange))
        {
            if (formation.OnFind && formation.Script != null && !formation.Alert)
            {
                var executingScript = new ScriptExecution(formation.Script);
                executingScript.RunToPauseOrFinish(_mapScriptContext!, _stateContext);
            }
            formation.Alert = true;
        }
    }

    private void ExecuteAllOnFindScripts() 
    {
        foreach (var actor in _currentMap!.Actors!) 
        {
            ExecuteOnFindScript(actor);
        }
    }

    private void ExecuteOnFindScript(Actor actor) 
    {
        if (
            actor.OnFind && 
            actor.Script != null && 
            _visionResolver!.CanSee(actor, _party, actor.TrackingRange)) 
        {
            var executingScript = new ScriptExecution(actor.Script);
            executingScript.RunToPauseOrFinish(_mapScriptContext!, _stateContext);
        }
    }

    private void CheckAnyFormationBesideParty()
    {
        for (int direction = -1; direction < 2; direction += 2)
        {
            var xFormation = _currentMap!.GetFormationAt(_party.XLocation + direction, _party.YLocation);
            var yFormation = _currentMap!.GetFormationAt(_party.XLocation, _party.YLocation + direction);
            if (xFormation != null && !xFormation.Dead) 
            {
                StartBattle(xFormation);
            } 
            else if (yFormation != null && !yFormation.Dead)
            {
                StartBattle(yFormation);
            }
        }
    }

    private void CheckFormationBesideParty(Formation formation)
    {
        var xDifference = formation.XLocation - _party.XLocation;
        var yDifference = formation.YLocation - _party.YLocation;
        if ((xDifference >= -1 && xDifference <= 1 && yDifference == 0) ||
            (yDifference >= -1 && yDifference <= 1 && xDifference == 0))
        {
            StartBattle(formation);
        }
    }

    private void StartBattle(Formation formation)
    {
        var battleStartRequest = new BattleStartRequest() 
        {
            Formation = formation
        };
        _gameObjects!.BattleStartRequest = battleStartRequest;
    }

    private void HandleMapChangeRequest(MapChangeRequest mapChangeRequest) 
    {
        _currentMap = _gameServices!.MapService.LoadMap(mapChangeRequest.MapName);
        _mapRenderer = _gameServices.MapRendererFactory!.Create(
            _currentMap.Tiles, 
            _currentMap.TileSheetName, 
            _currentMap.AnimatedTileSheetName, 
            _currentMap.MapBackgroundArtName
        );

        // Cast the list of actors and formations to the actor appearance shared interface.
        IEnumerable<IMapEntity> combined = _currentMap.Actors!.Cast<IMapEntity>().Concat(
            _currentMap.Formations!.Cast<IMapEntity>()
        );
        _actorRenderer = _gameServices.ActorRendererFactory!.Create([.. combined]);
        _walkAnimationManager = _gameServices.WalkAnimationManagerFactory!.Create([.. combined], _party);

        var navigationGrid = _gameServices.NavigationGridFactory!.Create(_currentMap);
        var visionResolverFactory = new VisionResolverFactory(navigationGrid);
        _visionResolver = visionResolverFactory.Create();

        _party.XLocation = mapChangeRequest.X;
        _party.YLocation = mapChangeRequest.Y;

        _movementResolver = _gameServices.MovementResolverFactory!.Create(_currentMap);
        _stateContext.InputContextManager.SetContext(new MapInputContext(_party, _movementResolver));
    }
}