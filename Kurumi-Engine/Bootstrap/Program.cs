// Engine classes.
using Config.Core;

using Data.Runtime.Actors.Factories;
using Data.Runtime.Formations.Factories;
using Data.Runtime.Maps.Factories;
using Data.Runtime.Party.Core;
using Data.Runtime.Party.Factory;

using Engine.Assets.Core;
using Engine.Context.Containers;
using Engine.Context.Core;
using Engine.Input.Context.Core;
using Engine.Input.Mapper;
using Engine.Input.System;
using Engine.State.Base;
using Engine.State.Core;
using Engine.State.States.Battle.Core;
using Engine.State.States.Maps.Core;
using Engine.Systems.Animation.Map.Factories;
using Engine.Systems.Combat.Factories;
using Engine.Systems.Movement.Factories;
using Engine.Systems.Navigation.Factories;
using Engine.Systems.Rendering.Factories;
using Engine.UI.Layout.Core;
using Engine.UI.Render;

using Game.Maps.Loader;
using Game.Maps.Registry;
using Game.Maps.Services;
using Game.Scripts.Library;

using Infrastructure.Database.Database;
using Infrastructure.Persistance.Base;
using Infrastructure.Persistance.Services;
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;


// External libraries.
using SFML.System;

namespace Bootstrap;

public static class Program 
{
    public static void Main(string[] args) 
    {
        var paths = BuildPaths();

        var gameData = BuildGameData(paths);
        var window = BuildWindow(gameData);
        var input = BuildInput();

        var config = gameData.ConfigProvider.GameConfig;

        var saveService = new SaveService(paths.SavePath);
        var saveData = saveService.LoadNewSaveData();
        var partyFactory = new PartyFactory(
            saveData.Characters, 
            gameData.GameDatabase.CharacterRegistry, 
            config.MaxPartySize,
            config.AgilityStatIndex
        );
        var party = partyFactory.Create(saveData.Party, saveData.Inventory);
        var gameServices = BuildGameServices(paths, input, gameData, saveService, party, window);

        var gameObjects = BuildGameObjects(gameServices, saveData, party);

        var gameContext = new GameContext() 
        {
            GameData = gameData, 
            GameObjects = gameObjects, 
            GameServices = gameServices
        };


        var stateContext = new StateContext() 
        {
            GameWindow = window, 
            InputContextManager = input.ContextManager
        };
        var stateManager = new StateManager(
            new MapState(gameContext, stateContext), 
            stateContext, 
            gameServices.InputMapper,
            gameServices.RenderSystem,
            gameServices.UIRenderSystem
        );

        RunGameLoop(window, input.System, stateManager, gameContext, stateContext);
    }

    private static Paths BuildPaths() 
    {
        var baseDir = AppContext.BaseDirectory;

        return new Paths 
        {
            RegistryRoot = Path.Combine(
                baseDir, 
                "Assets", 
                "Registry"
            ),
            SavePath = Path.Combine(
                baseDir, 
                "Saves"
            ), 
            ConfigPath = Path.Combine(
                baseDir, 
                "Config", 
                "yaml"
            )
        };
    }

    private static GameData BuildGameData(Paths paths) 
    {
        var configProvider = new ConfigProvider(paths.ConfigPath);
        var assetRegistryPath = Path.Combine(
            paths.RegistryRoot, 
            "asset_registry.json"
        );
        var fontRegistryPath  = Path.Combine(
            paths.RegistryRoot, 
            "font_registry.json"
        );

        return new GameData
        {
            ConfigProvider = configProvider, 
            GameDatabase = new GameDatabase(), 
            AssetRegistry = new AssetRegistry(assetRegistryPath, fontRegistryPath), 
            ScriptLibrary = new ScriptLibrary(paths.RegistryRoot)
        };
    }

    private static InputBundle BuildInput() 
    {
        var inputSystem = new InputSystem();
        var inputMapper = new InputMapper(inputSystem);
        var contextManager = new InputContextManager();

        return new InputBundle 
        {
            System = inputSystem, 
            Mapper = inputMapper, 
            ContextManager = contextManager
        };
    }

    private static GameServices BuildGameServices(
        Paths paths, 
        InputBundle input, 
        GameData gameData, 
        SaveService saveService, 
        Party party,
        GameWindow gameWindow) 
    {
        // Map services
        var mapRegistryPath = Path.Combine(paths.RegistryRoot, "map_registry.json");
        var scriptLibrary = gameData.ScriptLibrary;
        var tileRegistry = gameData.GameDatabase.TileRegistry;
        var database = gameData.GameDatabase;

        MapService mapService = new(
            new MapRegistry(mapRegistryPath), 
            new MapLoader(), 
            new MapFactory(database.ActorInfoRegistry, 
            tileRegistry, 
            new ActorFactory(scriptLibrary), 
            new DumbTrackingActorFactory(scriptLibrary), 
            new PathedActorFactory(scriptLibrary),
            new RandomActorFactory(scriptLibrary), 
            new SmartTrackingActorFactory(scriptLibrary), 
            party));

        // System factories.
        var damageCalculatorFactory = new DamageCalculatorFactory(database.StatShortNameIndex);

        // Render system.
        var renderSystem = new RenderSystem(gameWindow);

        // Map factories.
        var assetRegistry = gameData.AssetRegistry;
        var configProvider = gameData.ConfigProvider;
        var tileConfig = configProvider.TileSheetConfig;
        var animatedTileConfig = configProvider.AnimatedTileSheetConfig;
        var characterFieldSpriteConfig = configProvider.CharacterFieldSpriteConfig;

        var actorRendererFactory = new ActorRendererFactory(
            assetRegistry, 
            database.ActorSpriteRegistry,
            renderSystem,  
            tileConfig.Width, 
            tileConfig.Height
        );
        var mapAnimationManagerFactory = new MapAnimationManagerFactory(
            animatedTileConfig.AnimatedTileFrames, 
            animatedTileConfig.AnimationInterval
        );
        var mapRendererFactory = new MapRendererFactory(
            assetRegistry, 
            tileRegistry, 
            renderSystem, 
            tileConfig
        );
        var movementResolverFactory = new MovementResolverFactory(tileRegistry, party);
        var navigationGridFactory = new NavigationGridFactory(tileRegistry, party);
        var partyMapRendererFactory = new PartyMapRendererFactory(
            assetRegistry, 
            renderSystem, 
            database.CharacterRegistry, 
            characterFieldSpriteConfig, 
            tileConfig.Width, 
            tileConfig.Height
        );
        var walkAnimationManagerFactory = new WalkAnimationManagerFactory(
            characterFieldSpriteConfig.WalkAnimationFrames,
            characterFieldSpriteConfig.WalkAnimationSpeed
        );

        // Battle factories.
        var battleRendererFactory = new BattleRendererFactory(
            assetRegistry, 
            renderSystem, 
            configProvider.BattleBackgroundSpriteConfig,
            gameWindow.Size
        );
        var enemyRendererFactory = new EnemyRendererFactory(
            assetRegistry, 
            renderSystem, 
            configProvider.EnemyBattleSpriteConfig
        );
        var formationFactory = new FormationFactory(
            database.EnemyDefinitionRegistry, 
            database.EnemyBattleScriptRegistry, 
            database.EntityDefinitionRegistry,
            configProvider.GameConfig.AgilityStatIndex
        );
        var partyBattleRendererFactory = new PartyBattleRendererFactory(
            assetRegistry, 
            renderSystem,
            configProvider.CharacterBattleSpriteConfig
        );

        return new GameServices() 
        {
            MapService = mapService, 
            SaveService = saveService, 

            ScriptLibrary = new ScriptLibrary(paths.RegistryRoot), 

            InputMapper = input.Mapper,

            RenderSystem = renderSystem,
            UIRenderSystem = new UIRenderSystem(new UILayoutSystem()),

            DamageCalculator = damageCalculatorFactory.Create(),

            ActorRendererFactory = actorRendererFactory,
            MapAnimationManagerFactory = mapAnimationManagerFactory,
            MapRendererFactory = mapRendererFactory,
            MovementResolverFactory = movementResolverFactory,
            NavigationGridFactory = navigationGridFactory,
            PartyMapRendererFactory = partyMapRendererFactory,
            WalkAnimationManagerFactory = walkAnimationManagerFactory,

            BattleRendererFactory = battleRendererFactory,
            EnemyRendererFactory = enemyRendererFactory,
            FormationFactory = formationFactory,
            PartyBattleRendererFactory = partyBattleRendererFactory

        };
    }

    private static GameObjects BuildGameObjects(GameServices services, SaveData saveData, Party party) 
    {
        var mapService = services.MapService;
        var map = mapService.LoadMap(party.PartyModel.MapName);

        return new GameObjects{SaveData = saveData, Party = party, CurrentMap = map};
    }

    private static GameWindow BuildWindow(GameData data) 
    {
        var config = data.ConfigProvider.GameWindowConfig;
        return new GameWindow(config);
    }

    private static void RunGameLoop(
        GameWindow window, 
        InputSystem inputSystem, 
        StateManager stateManager, 
        GameContext gameContext,
        StateContext stateContext) 
    {
        var clock = new Clock();
        var gameObjects = gameContext.GameObjects;

        while (window.IsOpen) 
        {
            // Process state changes if requested.
            if (gameObjects.BattleStartRequest != null)
            {
                stateManager.ChangeState(new BattleState(
                    gameContext, 
                    stateContext, 
                    gameContext.GameObjects.Party, 
                    gameObjects.BattleStartRequest
                ));
                gameObjects.BattleStartRequest = null;
            }

            window.DispatchEvents();

            var deltaTime = clock.Restart().AsSeconds();

            inputSystem.Update();
            stateManager.Update(deltaTime);
        }
    }

    private sealed class Paths 
    {
        public required string RegistryRoot { get; init; }
        public required string SavePath { get; init; }
        public required string ConfigPath { get; init; }
    }

    private sealed class InputBundle 
    {
        public required InputSystem System { get; init; }
        public required InputMapper Mapper { get; init; }
        public required InputContextManager ContextManager { get; init; }
    }
}