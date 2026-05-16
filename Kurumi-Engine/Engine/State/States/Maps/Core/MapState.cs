using Data.Definitions.Actors.Base;
using Data.Definitions.Maps.Base;
using Data.Runtime.Actors.Core;
using Data.Runtime.Maps.Base;
using Data.Runtime.Maps.Core;
using Data.Runtime.Party.Core;
using Data.Runtime.Scripts.Execution;

using Engine.Context.Containers;
using Engine.Context.Core;
using Engine.Input.Context.Contexts;
using Engine.State.Base;
using Engine.Systems.Animation.Map.Core;
using Engine.Systems.Camera;
using Engine.Systems.Movement.Core;
using Engine.Systems.Perception.Core;
using Engine.Systems.Perception.Factories;
using Engine.Systems.Rendering.Core;

using Game.Scripts.Context.Builder.Core;
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

    public MapState(GameContext gameContext, StateContext stateContext) 
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
        _party = gameContext.GameObjects.Party;
    }

    public void OnEnter() 
    {
        // Break up functionality for readability.
        CacheDependencies();
        InitializeMap();
        InitializeInput();
        InitializeCamera();
    }

    public void OnExit() {}

    public void Update(float deltaTime) 
    {
        // Handle requested interactions.
        if (_stateContext.InputContextManager.GetGameplayContext()!.InteractRequested) 
        {
            _stateContext.InputContextManager.GetGameplayContext()!.InteractRequested = false;
            // Get location of party.
            var partyX = _party.XLocation;
            var partyY = _party.YLocation;
            var facing = _party.Facing;

            // Calculate the location being interacted with.
            var xChange = facing == (int) Direction.West ? -1 : facing == (int) Direction.East ? 1 : 0;
            var yChange = facing == (int) Direction.South ? 1 : facing == (int) Direction.North ? -1 : 0;
            var targetX = partyX + xChange;
            var targetY = partyY + yChange;
            var newFacing = facing == 
                (int) Direction.North ? (int) Direction.South 
                : facing == (int) Direction.East ? (int) Direction.West 
                : facing == (int) Direction.South ? (int) Direction.North 
                : (int) Direction.East;

            // Set new facing direction if the actor turns and activate script.
            var actors = _currentMap!.GetActorsAt(targetX, targetY);
            foreach (var actor in actors) 
            {
                if ((int) ActorBehaviour.StationaryDoesNotTurn != actor.Behaviour) 
                {
                    actor.Facing = newFacing;
                }

                // Load potential script and activate.
                if (actor.OnAction && actor.Script != null) 
                {
                    _stateContext.AddExecutingScript(new ScriptExecution(actor.Script));
                }
            }
        }

        // Handle on touch and on find scripts.
        var isCurrentlyMoving = _party.MovementProgress < 1f;
        var movementJustFinished = !isCurrentlyMoving && _party.MovingLastFrame;
        if (movementJustFinished) 
        {
            // Activate any on touch scripts on the tile.
            var actors = _currentMap!.GetActorsAt(_party.XLocation, _party.YLocation);
            foreach (var actor in actors) 
            {
                if (actor.OnTouch && actor.Script != null) 
                {
                    _stateContext.AddExecutingScript(new ScriptExecution(actor.Script));
                }
            }

            ExecuteAllOnFindScripts();

            _party.MovingLastFrame = false;
        }

        MoveAllActors(deltaTime);

        // Update animations.
        _walkAnimationManager!.Update(deltaTime);
        _mapAnimationManager!.Update(deltaTime);

        // Update camera's location.
        float interpolatedX = _party.LastX + (_party.PartyModel.XLocation - _party.LastX) * _party.MovementProgress;
        float interpolatedY = _party.LastY + (_party.PartyModel.YLocation - _party.LastY) * _party.MovementProgress;

        float worldX = interpolatedX * _tileWidth + _tileWidth / 2f;
        float worldY = interpolatedY * _tileHeight + _tileHeight / 2f;

        _camera!.Follow(worldX, worldY, _currentMap!.Width * _tileWidth, _currentMap.Height * _tileHeight);
        _stateContext.GameWindow.SetView(_camera.View);

        // Update the renderers and render.
        _mapRenderer!.Update(_mapAnimationManager, _camera.View);
        _actorRenderer!.Update(_camera.View);
        _partyMapRenderer!.Update(_camera.View);

        if (_gameObjects!.MapChangeRequest != null) 
        {
            HandleMapChangeRequest(_gameObjects.MapChangeRequest);
            _gameObjects.MapChangeRequest = null;
        }
    }

    public ScriptContext GetScriptContext() => _mapScriptContext!;

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
        var mapScriptContextBuilder = new MapScriptContextBuilder(_gameContext, _stateContext);
        _mapScriptContext = mapScriptContextBuilder.BuildScriptContext();
    }

    private void InitializeMap() 
    {
        // Map renderers.
        _actorRenderer = _gameServices!.ActorRendererFactory.Create(_currentMap!.Actors!);
        _mapAnimationManager = _gameServices.MapAnimationManagerFactory.Create();
        _mapRenderer = _gameServices.MapRendererFactory.Create(
            _currentMap.Tiles, 
            _currentMap.TileSheetName, 
            _currentMap.MapBackgroundArtName
        );
        _walkAnimationManager = _gameServices.WalkAnimationManagerFactory.Create(_currentMap!.Actors!, _party);

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
        var mapConfig = _gameData!.ConfigProvider.MapConfig;

        float viewWidth = mapConfig.MaxTilesWide * _tileWidth;
        float viewHeight = mapConfig.MaxTilesHigh * _tileHeight;

        _camera = new Camera(viewWidth, viewHeight);
        _stateContext.GameWindow.SetView(_camera.View);
    }

    private void MoveAllActors(float deltaTime) {
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
                    if (currentController.CanMove) 
                    {
                        // Execute move.
                        var move = currentController.GetMove(actor);
                        _movementResolver!.TryMove(actor, move);
                        currentController.ExecuteMove();

                        // Execute on find script.
                        ExecuteOnFindScript(actor);
                    }
                }
            }
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
            _stateContext.AddExecutingScript(new ScriptExecution(actor.Script));
        }
    }

    private void HandleMapChangeRequest(MapChangeRequest mapChangeRequest) 
    {
        _currentMap = _gameServices!.MapService.LoadMap(mapChangeRequest.MapName);
        _mapRenderer = _gameServices.MapRendererFactory!.Create(
            _currentMap.Tiles, 
            _currentMap.TileSheetName, 
            _currentMap.MapBackgroundArtName
        );
        _actorRenderer = _gameServices.ActorRendererFactory!.Create(_currentMap.Actors!);
        _walkAnimationManager = _gameServices.WalkAnimationManagerFactory!.Create(_currentMap.Actors!, _party);

        var navigationGrid = _gameServices.NavigationGridFactory!.Create(_currentMap);
        var visionResolverFactory = new VisionResolverFactory(navigationGrid);
        _visionResolver = visionResolverFactory.Create();

        _party.XLocation = mapChangeRequest.X;
        _party.YLocation = mapChangeRequest.Y;

        _movementResolver = _gameServices.MovementResolverFactory!.Create(_currentMap);
        _stateContext.InputContextManager.SetContext(new MapInputContext(_party, _movementResolver));
    }
}