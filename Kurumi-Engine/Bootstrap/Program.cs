// Config.
using Config.Core;

// Data.
using Data.Runtime.Actors.Factories;
using Data.Runtime.Formations.Factories;
using Data.Runtime.Maps.Factories;
using Data.Runtime.Parties.Core;
using Data.Runtime.Parties.Factory;

// Engine.
using Engine.Assets.Core;

using Engine.Context.Containers;
using Engine.Context.Core;

using Engine.Input.Context.Core;
using Engine.Input.Mapper;
using Engine.Input.System;

using Engine.State.Base;
using Engine.State.Core;
using Engine.State.States.Battle.Factories;
using Engine.State.States.Battle.Text.Factories;
using Engine.State.States.Maps.Factories;

using Engine.Systems.Animation.Map.Factories;
using Engine.Systems.Combat.Factories;
using Engine.Systems.Movement.Factories;
using Engine.Systems.Navigation.Factories;
using Engine.Systems.Rendering.Factories;

using Engine.UI.Layout.Core;
using Engine.UI.Render;

// Game.
using Game.Maps.Loader;
using Game.Maps.Registry;
using Game.Maps.Services;

using Game.Scripts.Context.Builder.Factories;
using Game.Scripts.Context.Capabilities.Implementations.Battle.Factories;
using Game.Scripts.Context.Capabilities.Implementations.Entity.Factories;
using Game.Scripts.Context.Capabilities.Implementations.Maps.Factories;
using Game.Scripts.Context.Capabilities.Implementations.Universal.Core;
using Game.Scripts.Library;

using Game.UI.Overlays.Factories;
using Game.UI.Views.Factories;

// Infrastructure.
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

        var configProvider = gameData.ConfigProvider;
        var gameConfig = configProvider.GameConfig;

        var saveService = new SaveService();
        var saveData = saveService.LoadNewSaveData();
        var partyFactory = new PartyFactory(
            saveData.CharacterCollection, 
            gameData.GameDatabase.CharacterRegistry, 
            gameConfig.MaxPartySize,
            gameConfig.AgilityStatIndex
        );
        var party = partyFactory.Create(
            saveData.Party, 
            saveData.Inventory, 
            configProvider.PartyMovementConfig.BaseMovementSpeed
        );
        var gameServices = BuildGameServices(paths, input, gameData, saveData, saveService, party, window);

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
        var displaySize = new Vector2u(
            (uint) configProvider.DisplayConfig.ViewWidth, 
            (uint) configProvider.DisplayConfig.ViewHeight 
        );

        // Create state factories and their dependencies.
        var gameDatabase = gameData.GameDatabase;
        var assetRegistry = gameData.AssetRegistry;

        var battleTextFactory = new BattleTextFactory(configProvider.BattleTextConfig);
        var battleViewFactory = new BattleViewFactory(
            assetRegistry, 
            gameDatabase.AbilityRegistry, 
            gameDatabase.AbilitySetRegistry, 
            configProvider.BattleWindowConfig, 
            configProvider.PartyChoicesConfig
        );

        var textWindowDefaults = configProvider.TextWindowDefaults;

        var choiceBoxWithDialogueOverlayFactory = new ChoiceBoxWithDialogueOverlayFactory(
            assetRegistry, 
            textWindowDefaults, 
            configProvider.ChoiceBoxDefaults
        );
        var dialogueOverlayFactory = new DialogueOverlayFactory(assetRegistry, textWindowDefaults);
        var dialogueWithNameBoxOverlayFactory = new DialogueWithNameBoxOverlayFactory(
            assetRegistry, 
            configProvider.NameBoxDefaults, 
            textWindowDefaults
        );
        var globalMessageFactory = new GlobalMessageFactory(assetRegistry, configProvider.GlobalMessageDefaults);

        var battleActionsFactory = new BattleActionsFactory(gameObjects);
        var mapNavigationActionsFactory = new MapNavigationActionsFactory(gameObjects);
        var movementActionsFactory = new MovementActionsFactory(party);
        var gameStateActionsFactory = new GameStateActionsFactory(saveData.GameVariables);
        var itemActionsFactory = new ItemActionsFactory(
            saveData.Inventory, 
            gameDatabase.ItemRegistry, 
            gameDatabase.ItemPoolRegistry
        );
        var partyStatusActionsFactory = new PartyStatusActionsFactory(party, gameDatabase.StatusRegistry);
        var uiActionsFactory = new UIActionsFactory(
            stateContext, 
            choiceBoxWithDialogueOverlayFactory, 
            dialogueOverlayFactory, 
            dialogueWithNameBoxOverlayFactory, 
            globalMessageFactory
        );

        var activeBattleActionsFactory = new ActiveBattleActionsFactory();
        var hpMpActionsFactory = new HpMpActionsFactory(party, gameServices.DamageCalculator);

        var mapScriptContextBuilderFactory = new MapScriptContextBuilderFactory(
            gameContext, 
            battleActionsFactory, 
            mapNavigationActionsFactory, 
            movementActionsFactory, 
            gameStateActionsFactory, 
            itemActionsFactory, 
            partyStatusActionsFactory, 
            uiActionsFactory
        );
        var battleScriptContextBuilderFactory = new BattleScriptContextBuilderFactory(
            activeBattleActionsFactory, 
            hpMpActionsFactory, 
            itemActionsFactory, 
            uiActionsFactory
        );

        var mapStateFactory = new MapStateFactory(gameContext, stateContext, mapScriptContextBuilderFactory);
        var battleStateFactory = new BattleStateFactory(
            gameContext, 
            stateContext, 
            party, 
            battleTextFactory, 
            battleViewFactory, 
            battleScriptContextBuilderFactory
        );

        var stateManager = new StateManager(
            mapStateFactory.Create(startingScript: null), 
            stateContext, 
            gameServices.InputMapper,
            gameServices.RenderSystem,
            gameServices.UIRenderSystem,
            displaySize
        );

        RunGameLoop(window, input.System, stateManager, gameContext, mapStateFactory, battleStateFactory);
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
        SaveData saveData, 
        SaveService saveService, 
        Party party,
        GameWindow gameWindow
    ) 
    {
        // Map services
        var mapRegistryPath = Path.Combine(paths.RegistryRoot, "map_registry.json");
        var scriptLibrary = gameData.ScriptLibrary;
        var tileRegistry = gameData.GameDatabase.TileRegistry;
        var database = gameData.GameDatabase;
        var configProvider = gameData.ConfigProvider;

        var formationFactory = new FormationFactory(
            party, 
            database.ActorInfoRegistry, 
            database.EnemyDefinitionRegistry, 
            database.EnemyBattleScriptRegistry, 
            database.EntityDefinitionRegistry, 
            scriptLibrary, 
            configProvider.GameConfig.AgilityStatIndex
        );

        MapService mapService = new(
            new MapRegistry(mapRegistryPath), 
            new MapLoader(), 

            new MapFactory(
                formationFactory, 
                database.MapFormationsIndex, 
                saveData.FormationCollection, 
                database.FormationRegistry, 
                database.ActorInfoRegistry, 
                tileRegistry, 
                saveData.ActorCollection, 
                new ActorFactory(scriptLibrary), 
                new DumbTrackingActorFactory(scriptLibrary), 
                new PathedActorFactory(scriptLibrary), 
                new RandomActorFactory(scriptLibrary), 
                new SmartTrackingActorFactory(scriptLibrary), 
                party
            )
        );

        // System factories.
        var damageCalculatorFactory = new DamageCalculatorFactory(database.StatShortNameIndex);

        // Render system.
        var renderSystem = new RenderSystem(gameWindow);

        // Map factories.
        var assetRegistry = gameData.AssetRegistry;
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
            characterFieldSpriteConfig.WalkAnimationFrames
        );

        // Battle factories.
        var virtualWindowConfig = configProvider.DisplayConfig;
        var battleRendererFactory = new BattleRendererFactory(
            assetRegistry, 
            renderSystem, 
            configProvider.BattleBackgroundSpriteConfig,
            new Vector2u((uint) virtualWindowConfig.ViewWidth, (uint) virtualWindowConfig.ViewHeight)
        );
        var enemyRendererFactory = new EnemyRendererFactory(
            assetRegistry, 
            renderSystem, 
            configProvider.BattleTextConfig, 
            configProvider.EnemyBattleSpriteConfig
        );
        var partyBattleRendererFactory = new PartyBattleRendererFactory(
            assetRegistry, 
            renderSystem, 
            configProvider.BattleTextConfig, 
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
        MapStateFactory mapStateFactory, 
        BattleStateFactory battleStateFactory
    ) 
    {
        var clock = new Clock();
        var gameObjects = gameContext.GameObjects;

        while (window.IsOpen) 
        {
            // Process state changes if requested.
            if (gameObjects.BattleStartRequest != null && stateManager.ReadyToChangeState())
            {
                stateManager.ChangeState(battleStateFactory.Create(gameObjects.BattleStartRequest));
                gameObjects.BattleStartRequest = null;
            }
            else if (gameObjects.BattleEndRequest != null)
            {
                stateManager.ChangeState(mapStateFactory.Create(gameObjects.BattleEndRequest.Script));
                gameObjects.BattleEndRequest = null;
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
        public required string ConfigPath { get; init; }
    }

    private sealed class InputBundle 
    {
        public required InputSystem System { get; init; }
        public required InputMapper Mapper { get; init; }
        public required InputContextManager ContextManager { get; init; }
    }
}