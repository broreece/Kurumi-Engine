using Config.Core;
using Config.Runtime.Map;
using Data.Definitions.Actors.Base;
using Data.Definitions.Maps.Base;
using Data.Definitions.Maps.Core;
using Data.Runtime.Actors.Core;
using Data.Runtime.Maps.Base;
using Data.Runtime.Maps.Core;
using Data.Runtime.Party.Core;
using Engine.Assets.Core;
using Engine.Context.Containers;
using Engine.Context.Core;
using Engine.Input.Context.Contexts;
using Engine.Input.Mapper;
using Engine.State.Base;
using Engine.Systems.Animation.Map.Core;
using Engine.Systems.Animation.Map.Factories;
using Engine.Systems.Camera;
using Engine.Systems.Movement.Core;
using Engine.Systems.Movement.Factories;
using Engine.Systems.Navigation.Factories;
using Engine.Systems.Perception.Core;
using Engine.Systems.Perception.Factories;
using Engine.Systems.Rendering.Core;
using Engine.Systems.Rendering.Factories;
using Game.Scripts.Context.Builder.Core;
using Game.Scripts.Context.Core;
using Infrastructure.Database.Base;
using Infrastructure.Rendering.Core;

namespace Engine.State.States;

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

    // Config.
    private ConfigProvider? _configProvider;
    private TileSheetConfig? _tileConfig;
    private CharacterFieldSpriteConfig? _characterFieldSpriteConfig;

    // Input mapper.
    private InputMapper? _inputMapper;

    // Registries.
    private AssetRegistry? _assetRegistry;
    private Registry<Tile>? _tileRegistry;

    // Map, movement resolver and vision resolver.
    private Map? _currentMap;
    private MovementResolver? _movementResolver;
    private VisionResolver? _visionResolver;

    // Cached variables.
    private int _tileWidth;
    private int _tileHeight;

    // Renderers.
    private RenderSystem? _renderSystem;
    private MapRenderer? _mapRenderer;
    private ActorRenderer? _actorRenderer;
    private PartyRenderer? _partyRenderer;
    private Camera? _camera;

    // Map factories.
    private MovementResolverFactory? _movementResolverFactory;
    private MapRendererFactory? _mapRendererFactory;
    private ActorRendererFactory? _actorRendererFactory;
    private WalkAnimationManagerFactory? _walkAnimationManagerFactory;
    private NavigationGridFactory? _navigationGridFactory;

    // Animation managers.
    private WalkAnimationManager? _walkAnimationManager;
    private MapAnimationManager? _mapAnimationManager;

    // Script context.
    private ScriptContext? _mapScriptContext;

    public MapState(GameContext gameContext, StateContext stateContext, Party party) 
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
        _party = party;
    }

    public void OnEnter() 
    {
        // Break up functionality for readability.
        CacheDependencies();
        InitializeRendering();
        InitializePartyRenderer();
        InitializeMap();
        InitializeInput();
        InitializeCamera();
    }

    public void OnExit() {}

    public void Update(float deltaTime) 
    {
        HandleInput();

        // Handle requested interactions.
        if (_stateContext.InputContextManager.GetGameplayContext()!.InteractRequested) 
        {
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
                if ((int) ActorBehaviour.StationaryDoesNotTurn != actor.ActorInfo.Behaviour) 
                {
                    actor.Facing = newFacing;
                }

                // Load potential script and activate.
                if (actor.ActorInfo.OnAction) 
                {
                    var scriptKey = actor.ActorInfo.ScriptName;
                    if (scriptKey != null) 
                    {
                        var script = _gameData!.ScriptLibrary.GetMapScript(scriptKey);
                        script.Activate(_mapScriptContext!);
                    }
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
                if (actor.ActorInfo.OnTouch) 
                {
                    var scriptKey = actor.ActorInfo.ScriptName;
                    if (scriptKey != null) 
                    {
                        var script = _gameData!.ScriptLibrary.GetMapScript(scriptKey);
                        script.Activate(_mapScriptContext!);
                    }
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

        _camera!.Follow(worldX, worldY, _currentMap!.GetWidth() * _tileWidth, _currentMap.GetHeight() * _tileHeight);
        _stateContext.GameWindow.SetView(_camera.View);

        // Update the renderers and render.
        _mapRenderer!.Update(_mapAnimationManager);
        _actorRenderer!.Update();
        _partyRenderer!.Update();
        _renderSystem!.Render();

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
        _configProvider = _gameData.ConfigProvider;
        _tileConfig = _configProvider.TileSheetConfig;
        _inputMapper = _gameContext.GameServices.InputMapper;
        _assetRegistry = _gameData.AssetRegistry;
        _tileRegistry = _gameData.GameDatabase.TileRegistry;
        _currentMap = _gameObjects.CurrentMap;

        // Variables.
        _tileWidth = _tileConfig.Width;
        _tileHeight = _tileConfig.Height;

        // Map script context.
        var mapScriptContextBuilder = new MapScriptContextBuilder(_gameContext);
        _mapScriptContext = mapScriptContextBuilder.BuildScriptContext();
    }

    private void InitializeRendering() => _renderSystem = new RenderSystem(_stateContext.GameWindow);

    private void InitializePartyRenderer() 
    {
        var characterRegistry = _gameData!.GameDatabase.CharacterRegistry;
        _characterFieldSpriteConfig = _configProvider!.CharacterFieldSpriteConfig;

        var partyRendererFactory = new PartyRendererFactory(
            _assetRegistry!, 
            _renderSystem!, 
            characterRegistry, 
            _characterFieldSpriteConfig, 
            _tileConfig!.Width, 
            _tileConfig.Height
        );
        _partyRenderer = partyRendererFactory.Create(_party);
    }

    private void InitializeMap() 
    {
        _mapRendererFactory = new MapRendererFactory(
            _assetRegistry!, 
            _tileRegistry!, 
            _renderSystem!, 
            _tileConfig!);
        _mapRenderer = _mapRendererFactory.Create(_currentMap!.GetTiles(), _currentMap.GetTileSheetName());

        _actorRendererFactory = new ActorRendererFactory(
            _assetRegistry!, 
            _gameData!.GameDatabase.ActorSpriteRegistry,
            _renderSystem!,  
            _tileConfig!.Width, 
            _tileConfig.Height);
        _actorRenderer = _actorRendererFactory.Create(_currentMap.GetActors());

        _walkAnimationManagerFactory = new WalkAnimationManagerFactory(
            _characterFieldSpriteConfig!.WalkAnimationFrames,
            _characterFieldSpriteConfig.WalkAnimationSpeed);
        _walkAnimationManager = _walkAnimationManagerFactory.Create(_currentMap!.GetActors(), _party);

        var mapAnimationManagerFactory = new MapAnimationManagerFactory(
            _configProvider!.AnimatedTileSheetConfig.AnimatedTileFrames, 
            _configProvider!.AnimatedTileSheetConfig.AnimationInterval
        );
        _mapAnimationManager = mapAnimationManagerFactory.Create();

        _navigationGridFactory = new NavigationGridFactory(_tileRegistry!, _party);
        var navigationGrid = _navigationGridFactory.Create(_currentMap);
        var visionResolverFactory = new VisionResolverFactory(navigationGrid);
        _visionResolver = visionResolverFactory.Create();
    }

    private void InitializeInput() 
    {
        _movementResolverFactory = new MovementResolverFactory(_tileRegistry!, _party);
        _movementResolver = _movementResolverFactory.Create(_gameObjects!.CurrentMap);
        _stateContext.InputContextManager.SetContext(new MapInputContext(_party, _movementResolver));
    }

    private void InitializeCamera() 
    {
        var mapConfig = _configProvider!.MapConfig;

        float viewWidth = mapConfig.MaxTilesWide * _tileWidth;
        float viewHeight = mapConfig.MaxTilesHigh * _tileHeight;

        _camera = new Camera(viewWidth, viewHeight);
        _stateContext.GameWindow.SetView(_camera.View);
    }

    private void HandleInput() 
    {
        var inputState = _inputMapper!.BuildState();
        _stateContext.InputContextManager.Update(inputState);
    }

    private void MoveAllActors(float deltaTime) {
        // Move all actors.
        foreach (var actor in _currentMap!.GetActors()) 
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
                    if (currentController.CanMove()) 
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
        foreach (var actor in _currentMap!.GetActors()) 
        {
            ExecuteOnFindScript(actor);
        }
    }

    private void ExecuteOnFindScript(Actor actor) 
    {
        if (actor.ActorInfo.OnFind && _visionResolver!.CanSee(actor, _party, actor.ActorInfo.TrackingRange)) 
        {
            var scriptKey = actor.ActorInfo.ScriptName;
            if (scriptKey != null) 
            {
                var script = _gameData!.ScriptLibrary.GetMapScript(scriptKey);
                script.Activate(_mapScriptContext!);
            }
        }
    }

    private void HandleMapChangeRequest(MapChangeRequest mapChangeRequest) 
    {
        _currentMap = _gameContext.GameServices.MapService.LoadMap(mapChangeRequest.MapName);
        _mapRenderer = _mapRendererFactory!.Create(_currentMap.GetTiles(), _currentMap.GetTileSheetName());
        _actorRenderer = _actorRendererFactory!.Create(_currentMap.GetActors());
        _walkAnimationManager = _walkAnimationManagerFactory!.Create(_currentMap.GetActors(), _party);

        var navigationGrid = _navigationGridFactory!.Create(_currentMap);
        var visionResolverFactory = new VisionResolverFactory(navigationGrid);
        _visionResolver = visionResolverFactory.Create();

        _party.XLocation = mapChangeRequest.X;
        _party.YLocation = mapChangeRequest.Y;

        _movementResolver = _movementResolverFactory!.Create(_currentMap);
        _stateContext.InputContextManager.SetContext(new MapInputContext(_party, _movementResolver));
    }
}