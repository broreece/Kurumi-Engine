// Engine classes.
using Config.Core;

using Data.Runtime.Actors.Factories;
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

        var saveService = new SaveService(paths.SavePath);
        var saveData = saveService.LoadNewSaveData();
        var partyFactory = new PartyFactory(
            saveData.Characters, 
            gameData.GameDatabase.CharacterRegistry, 
            gameData.ConfigProvider.GameConfig.MaxPartySize
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
            new MapState(gameContext, stateContext, party), 
            stateContext, 
            gameServices.InputMapper,
            gameServices.RenderSystem
        );

        RunGameLoop(window, input.System, stateManager, gameContext, stateContext, party);
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
        var mapRegistryPath = Path.Combine(paths.RegistryRoot, "map_registry.json");
        MapService mapService = new(
            new MapRegistry(mapRegistryPath), 
            new MapLoader(), 
            new MapFactory(gameData.GameDatabase.ActorInfoRegistry, 
            gameData.GameDatabase.TileRegistry, 
            new ActorFactory(), 
            new DumbTrackingActorFactory(), 
            new PathedActorFactory(),
            new RandomActorFactory(), 
            new SmartTrackingActorFactory(), 
            party));

        return new GameServices() 
        {
            MapService = mapService, 
            SaveService = saveService, 
            ScriptLibrary = new ScriptLibrary(paths.RegistryRoot), 
            InputMapper = input.Mapper,
            RenderSystem = new RenderSystem(gameWindow)
        };
    }

    private static GameObjects BuildGameObjects(GameServices services, SaveData saveData, Party party) 
    {
        var mapService = services.MapService;
        var map = mapService.LoadMap(party.PartyModel.MapName);

        return new GameObjects{SaveData = saveData, CurrentMap = map};
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
        StateContext stateContext,
        Party party) 
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
                    party, 
                    gameObjects.BattleStartRequest
                ));
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