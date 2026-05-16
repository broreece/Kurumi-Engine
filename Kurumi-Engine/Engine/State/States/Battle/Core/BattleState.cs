using Data.Definitions.Entities.Abilities.Core;
using Data.Runtime.Entities.Base;
using Data.Runtime.Formations.Base;
using Data.Runtime.Formations.Core;
using Data.Runtime.Party.Core;
using Data.Runtime.Scripts.Execution;

using Engine.Context.Core;
using Engine.Input.Context.Contexts;
using Engine.State.Base;
using Engine.State.States.Battle.Base;
using Engine.State.States.Battle.Exceptions;
using Engine.Systems.Camera;
using Engine.Systems.Rendering.Core;
using Engine.UI.Elements;
using Engine.UI.Render;

using Game.Scripts.Context.Builder.Core;
using Game.Scripts.Context.Core;
using Game.Scripts.Context.Variables.Base;
using Game.Scripts.Library;
using Game.UI.Views;

using Infrastructure.Database.Base;
using Infrastructure.Rendering.Core;

namespace Engine.State.States.Battle.Core;

/// <summary>
/// Renders the enemy formation, parties and attacking animations. 
/// Contains input handler and systems relating to the battle state menus.
/// </summary>
public sealed class BattleState : IGameState, IBattleMenu 
{
    // Context.
    private readonly GameContext _gameContext;
    private readonly StateContext _stateContext;

    // Registries for abilities and skills.
    private readonly Registry<AbilityDefinition> _abilityRegistry;

    // Party.
    private readonly Party _party;

    // Enemy formation.
    private readonly Formation _formation;

    // Battle variable.
    private readonly BattleStartRequest _battle;

    // UI elements.
    private readonly UIElement _uiRoot;
    private readonly BattleView _view;

    // UI renderer.
    private readonly UIRenderSystem _uiRenderSystem;

    // Cached config.
    private readonly int _maxChoicesPerPage;

    // Current selection indexes.
    private int _currentCharacterIndex = 0;
    private int _currentSelectionIndex = 0;
    private int _currentTargetIndex;

    // Current turn count.
    private int _currentTurn = 1;

    // Current selection state.
    private bool _targetSelector = false;

    // Render system.
    private RenderSystem? _renderSystem;

    // Renderers.
    private BattleRenderer? _battleRenderer;
    private EnemyRenderer? _enemyRenderer;
    private PartyBattleRenderer? _partyBattleRenderer;

    // Camera.
    private Camera? _camera;

    // Script context and library.
    private ScriptContext? _battleScriptContext;
    private ScriptLibrary? _scriptLibrary;

    // List of actions to be executed in order.
    private PriorityQueue<BattleAction, int> _actions = new(Comparer<int>.Create((x, y) => y.CompareTo(x)));
    private Stack<(BattleAction, int)> _queuedActions = [];

    private int GetNumberOfTargets() => _party.Size + _formation.GetAmountOfLivingEnemies();

    private bool WonBattle => _formation.IsDefeated();

    private bool LostBattle => _party.LeadersHp == 0;

    public BattleState(GameContext gameContext, StateContext stateContext, Party party, BattleStartRequest battle) 
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
        _party = party;
        _currentTargetIndex = party.Size;
        _battle = battle;

        var gameData = _gameContext.GameData;
        var gameServices = _gameContext.GameServices;
        var database = gameData.GameDatabase;
        var formationRegistry = database.FormationRegistry;
        var saveData = gameContext.GameObjects.SaveData;
        var configProvider = gameContext.GameData.ConfigProvider;

        _abilityRegistry = database.AbilityRegistry;

        var formationModel = saveData.Formations[battle.EnemyFormationId];
        var formationDefinition = formationRegistry.Get(battle.EnemyFormationId);

        _formation = gameServices.FormationFactory.Create(formationDefinition, formationModel);

        var battleWindowConfig = configProvider.BattleWindowConfig;
        _maxChoicesPerPage = battleWindowConfig.MaxChoicesPerPage;

        _view = new BattleView(
            gameData.AssetRegistry, 
            _abilityRegistry,
            database.AbilitySetRegistry,
            battleWindowConfig,
            configProvider.PartyChoicesConfig,
            party.Characters
        );
        _uiRoot = _view.UIElement;
        _uiRenderSystem = gameServices.UIRenderSystem;
    }

    public void OnEnter()
    {
        CacheDependencies();
        InitializeInput();
    }

    public void OnExit() {}

    public void Update(float deltaTime)
    {
        _battleRenderer!.Update(_camera!.View);
        _enemyRenderer!.Update(_camera.View, _targetSelector, _currentTargetIndex - _party.Size);
        _partyBattleRenderer!.Update(_camera.View, _targetSelector, _currentTargetIndex);

        // Update the UI then render the UI.
        _view.Update(_currentCharacterIndex, _currentSelectionIndex);
        _uiRenderSystem.Render(_uiRoot, _renderSystem!, _stateContext.GameWindow.Size);
    }

    public ScriptContext GetScriptContext() => _battleScriptContext!;

    public void MoveUp()
    {
        _currentSelectionIndex = _currentSelectionIndex == 0 ? _maxChoicesPerPage - 1: _currentSelectionIndex - 1;
    }

    public void MoveDown()
    {
        _currentSelectionIndex = _currentSelectionIndex == _maxChoicesPerPage - 1 ? 0 : _currentSelectionIndex + 1;
    }

    public void MoveRight()
    {
        if (_targetSelector)
        {
            var nextTargetIndex = _currentTargetIndex;

            do
            {
                nextTargetIndex ++;
                
                if (nextTargetIndex >= _party.Size + _formation.Enemies.Count)
                {
                    nextTargetIndex = _currentTargetIndex;
                    break;
                }
            }
            while (!IsValidTarget(nextTargetIndex));

            _currentTargetIndex = nextTargetIndex;
        }
        else
        {
            // TODO: Implement next page.
        }
    }

    public void MoveLeft()
    {
        if (_targetSelector)
        {
            var nextTargetIndex = _currentTargetIndex;

            do
            {
                nextTargetIndex --;
                
                if (nextTargetIndex < 0)
                {
                    nextTargetIndex = _currentTargetIndex;
                    break;
                }
            }
            while (!IsValidTarget(nextTargetIndex));

            _currentTargetIndex = nextTargetIndex;
        }
        else
        {
            // TODO: Implement previous page.
        }
    }

    private bool IsValidTarget(int currentIndex)
    {
        if (currentIndex < _party.Size)
        {
            return _party.Characters[currentIndex].CurrentHP > 0;
        }
        return _formation.StoredEntityData[currentIndex - _party.Size].Entity.CurrentHP > 0;
    }

    private int GetFirstValidEnemyTarget()
    {
        int enemyIndex = 0;
        do
        {
            enemyIndex ++;
        }
        while (enemyIndex < _formation.GetAmountOfLivingEnemies() + 1 && 
            _formation.GetEntityAt(enemyIndex).CurrentHP == 0);
        return enemyIndex;
    }

    private int GetFirstValidPartyTarget()
    {
        int characterIndex = 0;
        do
        {
            characterIndex ++;
        }
        while (characterIndex < _party.Size &&
            _party.Characters[characterIndex].CurrentHP == 0);
        return characterIndex;
    }

    public void Confirm()
    {
        // Check if in target selector state or the menu selector state.
        if (_targetSelector)
        {
            // Check for abilities.
            var currentCharacter = _party.Characters[_currentCharacterIndex];
            var abilities = currentCharacter.GetAbilityIDs();
            if (_currentSelectionIndex < abilities.Count)
            {
                _queuedActions.Push((new BattleAction()
                {
                    UserIndex = _currentCharacterIndex,
                    TargetIndex = _currentTargetIndex,
                    IsEnemy = false,
                    ScriptName = _abilityRegistry.Get(abilities[_currentSelectionIndex]).ScriptName 
                        ?? throw new AbilityHasNoScriptException($"The ability of user {_currentCharacterIndex} has " +
                        $"no assoicated script for the ability: {_currentSelectionIndex}.")
                }, 
                currentCharacter.BattleSpeed));
            }

            // TODO: Implement skills and hard coded values here.

            NextBattler();
        }
        else
        {
            _targetSelector = true;
        }
    }

    public void Cancel()
    {
        if (_targetSelector)
        {
            _targetSelector = false;
        }
        else if (_queuedActions.Count > 0)
        {
            _queuedActions.Pop();
            _currentCharacterIndex --;
        }
    }

    private void CacheDependencies() 
    {
        var gameServices = _gameContext.GameServices;

        var gameWindow = _stateContext.GameWindow;
        var windowSize = gameWindow.Size;

        // Renderer.
        _renderSystem = gameServices.RenderSystem;

        // Camera
        _camera = new Camera(windowSize.X, windowSize.Y);
        gameWindow.SetView(_camera.View);

        // Renderers.
        _battleRenderer = gameServices.BattleRendererFactory.Create(_battle.BattleBackgroundArtName);
        _enemyRenderer = gameServices.EnemyRendererFactory.Create(_formation);
        _partyBattleRenderer = gameServices.PartyBattleRendererFactory.Create(_party);

        // Battle script context and script library.
        var battleScriptContextBuilder = new BattleScriptContextBuilder(
            _gameContext, 
            _stateContext, 
            _party, 
            _formation
        );
        _battleScriptContext = battleScriptContextBuilder.BuildScriptContext();
        _scriptLibrary = gameServices.ScriptLibrary;
    }

    private void InitializeInput()
    {
         _stateContext.InputContextManager.SetContext(new BattleInputContext(this));
    }

    /// <summary>
    /// Moves the current turn to the next possible party member or initiates the enemy phase if final character.
    /// </summary>
    private void NextBattler()
    {
        // Check if it's enemies turns.
        if (_currentCharacterIndex == _party.Size - 1)
        {
            _currentCharacterIndex = 0;
            QueueAllActions();
            ConductEnemyPhase();
        }
        else
        {
            _currentCharacterIndex ++;
        } 
        _currentSelectionIndex = 0;
        _currentTargetIndex = GetFirstValidEnemyTarget();
    }

    /// <summary>
    /// Empties the actions stack and moves it into the actions queue with associated priority.
    /// </summary>
    private void QueueAllActions()
    {
        while (_queuedActions.Count > 0)
        {
            var (action, priority) = _queuedActions.Pop();
            _actions.Enqueue(action, priority);
        }
    }

    /// <summary>
    /// Loops through all enemies battle scripts to evaluate which actions to queue this turn.
    /// </summary>
    private void ConductEnemyPhase()
    {
        int entityIndex = 0;
        foreach (var enemy in _formation.Enemies)
        {
            if (_formation.GetEntityAt(entityIndex).CurrentHP > 0)
            {
                foreach (var enemyBattleScript in enemy.BattleScripts)
                {
                    /// Condition for if script is queued is:
                    /// 1. On the start turn of the battle script.
                    /// 2. On any turn where the turn count mod by the frequency is 0 (every 2 turns for example).
                    if (enemyBattleScript.StartTurn == _currentTurn ||
                        ((enemyBattleScript.StartTurn <= _currentTurn) && 
                        (_currentTurn % enemyBattleScript.Frequency == 0)))
                    {
                        _actions.Enqueue(new BattleAction()
                        {
                            UserIndex = entityIndex,
                            TargetIndex = enemyBattleScript.Target,
                            IsEnemy = true,
                            ScriptName = enemyBattleScript.ScriptName
                        }
                        , _formation.GetEntityAt(entityIndex).BattleSpeed);
                    }
                }
            }
            entityIndex ++;
        }

        ExecuteActions();
    }

    /// <summary>
    /// Loops through all queued actions, emptying the priority queue along the way.
    /// </summary>
    private void ExecuteActions()
    {
        while (_actions.Count > 0)
        {
            BattleAction action = _actions.Dequeue();
            Execute(action);
        }
        
        _currentTurn ++;
        _targetSelector = false;
    }

    private void Execute(BattleAction action)
    {
        var script = _scriptLibrary!.GetEntityScript(action.ScriptName);
        EntityIndex user, target;

        // Account for random attack indexes.
        var targetIndex = action.TargetIndex;
        switch (targetIndex)
        {
            case (int) BattleTargets.RandomPartyMember:
                // Generate a random alive party member, keep trying to ensure the party member is alive.
                var random = new Random();
                do
                {
                    targetIndex = random.Next(0, _party.Size);
                }
                while (_party.Characters[targetIndex].CurrentHP <= 0);
                break;

            default:
                break;
        }

        // Check if the user is an enemy or not.
        if (action.IsEnemy)
        {
            user = new EntityIndex() 
            { 
                Index = action.UserIndex - _party.Size, 
                EntityType = EntityType.Enemy 
            };
        }
        else
        {
            user = new EntityIndex() 
            { 
                Index = action.UserIndex, 
                EntityType = EntityType.Character 
            };
        }

        // If target is enemy.
        if (action.TargetIndex >= _party.Size)
        {
            target = new EntityIndex() 
            { 
                Index = targetIndex - _party.Size, 
                EntityType = EntityType.Enemy 
            };
        }
        else
        {
            target = new EntityIndex() 
            { 
                Index = targetIndex,  
                EntityType = EntityType.Character 
            };
        }

        // Add user and target to script context.
        _battleScriptContext!.SetVariable(ScriptVariables.User, user);
        _battleScriptContext.SetVariable(ScriptVariables.Target, target);

        // Add to executing scripts.
        var scriptExceution = new ScriptExecution(script);
        scriptExceution.RunToPauseOrFinish(_battleScriptContext, _stateContext);

        // Check if target is enemy and died, if they died execute on kill script.
        if (target.EntityType == EntityType.Enemy && _formation.GetEntityAt(target.Index).CurrentHP <= 0)
        {
            var onKillScriptName = _formation.Enemies[target.Index].OnKillScript;
            if (onKillScriptName != null)
            {
                var onKillScript = _scriptLibrary.GetBattleScript(onKillScriptName);
                var onKillScriptExecution = new ScriptExecution(onKillScript);
                onKillScriptExecution.RunToPauseOrFinish(_battleScriptContext, _stateContext);
            }
        }
    }
}