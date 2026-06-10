// Data.
using Data.Definitions.Entities.Abilities.Core;

using Data.Runtime.Entities.Base;
using Data.Runtime.Formations.Base;
using Data.Runtime.Formations.Core;
using Data.Runtime.Maps.Base.Change;
using Data.Runtime.Parties.Core;
using Data.Runtime.Scripts.Execution;

// Engine.
using Engine.Context.Core;

using Engine.Input.Context.Contexts;

using Engine.State.Base;
using Engine.State.States.Battle.Base;
using Engine.State.States.Battle.Exceptions;
using Engine.State.States.Battle.Text.Base;
using Engine.State.States.Battle.Text.Core;
using Engine.State.States.Battle.Text.Factories;

using Engine.Systems.Camera;
using Engine.Systems.Rendering.Core;

using Engine.UI.Elements;
using Engine.UI.Render;

// Game.
using Game.Scripts.Context.Builder.Factories;
using Game.Scripts.Context.Core;
using Game.Scripts.Context.Variables.Base;
using Game.Scripts.Library;

using Game.UI.Views.Core;
using Game.UI.Views.Factories;

// Infrastructure.
using Infrastructure.Database.Base;
using Infrastructure.Rendering.Core;

// External libraries.
using SFML.System;

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

    // UI elements.
    private readonly UIElement _uiRoot;
    private readonly BattleView _view;

    // UI renderer.
    private readonly UIRenderSystem _uiRenderSystem;

    // Battle text factory.
    private readonly BattleTextFactory _battleTextFactory;

    // Context builder.
    private readonly BattleScriptContextBuilderFactory _battleScriptContextBuilderFactory;

    // List of battle text.
    private readonly IList<BattleText> _battleText = [];

    // Cached config.
    private readonly int _maxChoicesPerPage;
    private readonly bool _itemsEnabled;
    private readonly bool _runAwayEnabled;
    private Vector2u? _displaySize;

    // Current selection indexes.
    private int _currentCharacterIndex = 0;
    private int _currentSelectionIndex;
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

    private bool WonBattle => _formation.IsDefeated();

    private bool LostBattle => _party.LeadersHp == 0;

    public ScriptContext ScriptContext => _battleScriptContext!;

    internal BattleState(
        GameContext gameContext, 
        StateContext stateContext, 
        Party party, 
        BattleTextFactory battleTextFactory, 
        BattleViewFactory battleViewFactory, 
        BattleScriptContextBuilderFactory battleScriptContextBuilderFactory, 
        BattleStartRequest battle
    ) 
    {
        _gameContext = gameContext;
        _stateContext = stateContext;
        _party = party;
        _battleTextFactory = battleTextFactory;
        _battleScriptContextBuilderFactory = battleScriptContextBuilderFactory;

        _currentTargetIndex = party.Size;

        var gameData = _gameContext.GameData;
        var gameServices = _gameContext.GameServices;
        var database = gameData.GameDatabase;
        var configProvider = gameContext.GameData.ConfigProvider;

        _abilityRegistry = database.AbilityRegistry;

        var battleWindowConfig = configProvider.BattleWindowConfig;
        _maxChoicesPerPage = battleWindowConfig.MaxChoicesPerPage;

        // Assign party choices config.
        var partyChoicesConfig = configProvider.PartyChoicesConfig;
        _itemsEnabled = partyChoicesConfig.ItemsEnabled;
        _runAwayEnabled = partyChoicesConfig.RunAwayEnabled;

        _view = battleViewFactory.Create(party.Characters);
        _uiRoot = _view.UIElement;
        _uiRenderSystem = gameServices.UIRenderSystem;

        // Check if the battle started from a formation encounter or a script.
        if (battle.Formation != null)
        {
            _formation = battle.Formation;
        }
        else
        {
            var formationRegistry = database.FormationRegistry;
            var saveData = gameContext.GameObjects.SaveData;

            var formationModel = saveData.Formations[battle.EnemyFormationId!];
            var formationDefinition = formationRegistry.Get(battle.EnemyFormationId);

            _formation = gameServices.FormationFactory.Create(formationDefinition, formationModel, null);
        }
    }

    public void OnEnter()
    {
        CacheDependencies();
        InitializeInput();
    }

    public void OnExit() {}

    public void Update(float deltaTime)
    {
        _battleRenderer!.Update(_camera!.View, deltaTime);
        _enemyRenderer!.Update(_camera.View, _targetSelector, _currentTargetIndex - _party.Size);
        _partyBattleRenderer!.Update(_camera.View, _targetSelector, _currentTargetIndex);

        // Update the UI then render the UI.
        _view.Update(_currentCharacterIndex, _currentSelectionIndex);
        _uiRenderSystem.Render(
            _uiRoot, 
            _renderSystem!, 
            _displaySize ?? 
                throw new DisplayConfigNotSetException("Display size field was not assigned in battle state."), 
            _stateContext.GameWindow.Size    
        );
    }

    public void MoveUp()
    {
        _currentSelectionIndex = _currentSelectionIndex == 0 ? GetNumberOfOptions() : _currentSelectionIndex - 1;
    }

    public void MoveDown()
    {
        _currentSelectionIndex = _currentSelectionIndex == GetNumberOfOptions() ? 0 : _currentSelectionIndex + 1;
    }

    public void MoveRight()
    {
        if (_targetSelector)
        {
            // Check we are not targetting a special target.
            if (_currentTargetIndex >= 0)
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
            // Check we are not targetting a special target.
            if (_currentTargetIndex >= 0)
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
        
        while (characterIndex < _party.Size - 1 && _party.Characters[characterIndex].CurrentHP == 0)
        {
            characterIndex++;
        }

        return characterIndex;
    }

    public void Confirm()
    {
        // Check if in target selector state or the menu selector state.
        if (_targetSelector)
        {
            // Check for abilities.
            var currentCharacter = _party.Characters[_currentCharacterIndex];
            var abilities = currentCharacter.AbilityIDs;
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
            var currentCharacter = _party.Characters[_currentCharacterIndex];
            var abilities = currentCharacter.AbilityIDs;
            if (_currentSelectionIndex < abilities.Count)
            {
                var ability = _abilityRegistry.Get(abilities[_currentSelectionIndex]);
                bool targetsParty = ability.DefaultTargetParty;
                bool targetsAll = ability.TargetsAll;
                bool randomTarget = ability.RandomTarget;

                if (targetsParty)
                {
                    if (targetsAll)
                    {
                        _currentTargetIndex = (int) BattleTargets.AllPartyMembers;
                    }
                    else if (randomTarget)
                    {
                        _currentTargetIndex = (int) BattleTargets.RandomPartyMember;
                    }
                    else
                    {
                        _currentTargetIndex = GetFirstValidPartyTarget();
                    }
                }
                else
                {
                    if (targetsAll)
                    {
                        _currentTargetIndex = (int) BattleTargets.AllEnemies;
                    }
                    else if (randomTarget)
                    {
                        _currentTargetIndex = (int) BattleTargets.RandomEnemy;
                    }
                    else
                    {
                        _currentTargetIndex = GetFirstValidEnemyTarget();
                    }
                }
            }
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
        var displayConfig = _gameContext.GameData.ConfigProvider.DisplayConfig;
        var displayWidth = displayConfig.ViewWidth;
        var displayHeight = displayConfig.ViewHeight;
        _displaySize = new Vector2u((uint) displayWidth, (uint) displayHeight);

        // Renderer.
        _renderSystem = gameServices.RenderSystem;

        // Camera
        _camera = new Camera(displayWidth, displayHeight);
        gameWindow.SetView(_camera.View);

        // Renderers.
        _battleRenderer = gameServices.BattleRendererFactory.Create(_formation.BackgroundArtName, _battleText);
        _enemyRenderer = gameServices.EnemyRendererFactory.Create(_formation);
        _partyBattleRenderer = gameServices.PartyBattleRendererFactory.Create(_party);

        // Battle script context and script library.
        var battleScriptContextBuilder = _battleScriptContextBuilderFactory.Create(_formation);
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
            CheckIfBattleEnded();
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
            var targets = GetTargets(action.TargetIndex);
            ExecuteActionOnTargets(action, targets);
        }
        
        _currentTurn ++;
        _targetSelector = false;
    }

    private IReadOnlyList<int> GetTargets(int targetIndex)
    {
        var targets = new List<int>();
        var random = new Random();

        switch (targetIndex)
        {
            case (int) BattleTargets.RandomPartyMember:
                // Generate a random alive party member, keep trying to ensure the party member is alive.
                do
                {
                    targetIndex = random.Next(0, _party.Size);
                }
                while (_party.Characters[targetIndex].CurrentHP < 0);

                targets.Add(targetIndex);

                break;

            case (int) BattleTargets.RandomEnemy:
                // Generate a random alive enemy, keep trying to ensure the enemy is alive.
                do
                {
                    targetIndex = random.Next(0, _formation.Enemies.Count);
                }
                while (_formation.GetAmountOfLivingEnemies() >= 1 && 
                    _formation.GetEntityAt(targetIndex).CurrentHP < 0);

                targets.Add(targetIndex + _party.Size);

                break;

            case (int) BattleTargets.AllEnemies:
                // Store all alive enemy indexes inside a list.
                for (int enemyIndex = 0; enemyIndex < _formation.Enemies.Count; enemyIndex ++)
                {
                    if (_formation.GetEntityAt(enemyIndex).CurrentHP > 0)
                    {
                        targets.Add(enemyIndex + _party.Size);
                    }
                }

                break;

            case (int) BattleTargets.AllPartyMembers:
                // Store all alive party member indexes inside a list.
                for (int partyIndex = 0; partyIndex < _party.Size; partyIndex ++)
                {
                    if (_party.Characters[partyIndex].CurrentHP > 0)
                    {
                        targets.Add(partyIndex);
                    }
                }

                break;

            default:
                targets.Add(targetIndex);
                break;
        }

        return targets;
    }

    private void ExecuteActionOnTargets(BattleAction action, IReadOnlyList<int> targetIndexes)
    {
        EntityIndex user, target;

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

        foreach (var targetIndex in targetIndexes)
        {
            IStats targetStats;
            // If target is enemy.
            if (targetIndex >= _party.Size)
            {
                target = new EntityIndex() 
                { 
                    Index = targetIndex - _party.Size, 
                    EntityType = EntityType.Enemy 
                };
                targetStats = _formation.GetEntityAt(targetIndex - _party.Size);
            }
            else
            {
                target = new EntityIndex() 
                { 
                    Index = targetIndex,  
                    EntityType = EntityType.Character 
                };
                targetStats = _party.Characters[targetIndex];
            }
            ExecuteAction(user, target, action.ScriptName, targetStats);
        }
    }

    private void ExecuteAction(EntityIndex user, EntityIndex target, string scriptName, IStats targetStats) 
    {
        // Load target HP before executing action.
        int beforeHp = targetStats.CurrentHP;

        var script = _scriptLibrary!.GetEntityScript(scriptName);

        _battleScriptContext!.SetVariable(ScriptVariables.User, user);
        _battleScriptContext.SetVariable(ScriptVariables.Target, target);

        // Add to executing scripts.
        var scriptExceution = new ScriptExecution(script);
        scriptExceution.RunToPauseOrFinish(_battleScriptContext, _stateContext);

        // Load damage text.
        int afterHp = targetStats.CurrentHP;
        int damage = beforeHp - afterHp;
        if (damage > 0)
        {
            // TODO: (DD-02) Correct placement here.
            _battleText.Add(_battleTextFactory.Create(damage.ToString(), 0, 0, BattleTextType.Damage));
        }
        else
        {
            // TODO: (DD-02) Correct placement here.
            _battleText.Add(_battleTextFactory.Create((damage * - 1).ToString(), 0, 0, BattleTextType.Heal));
        }

        // Check if target is enemy and died, if they died execute on kill script.
        // Also check if target is main part of any other body part and kill linked body parts.
        if (target.EntityType == EntityType.Enemy && _formation.GetEntityAt(target.Index).CurrentHP <= 0)
        {
            // Check for on kill script.
            var onKillScriptName = _formation.Enemies[target.Index].OnKillScript;
            if (onKillScriptName != null)
            {
                var onKillScript = _scriptLibrary.GetBattleScript(onKillScriptName);
                var onKillScriptExecution = new ScriptExecution(onKillScript);
                onKillScriptExecution.RunToPauseOrFinish(_battleScriptContext, _stateContext);
            }

            // Check for connected body parts.
            for (int index = 0; index < _formation.Enemies.Count; index ++)
            {
                if (_formation.Enemies[index].MainPart - 1 == target.Index)
                {
                    _formation.GetEntityAt(index).CurrentHP = 0;
                }
            }
        }
    }

    private void CheckIfBattleEnded()
    {
        if (LostBattle)
        {
            if (_formation.HasOnLoseScript)
            {
                _gameContext.GameObjects.BattleEndRequest = new BattleEndRequest() { Script = _formation.OnLoseScript };
            }
            else
            {
                // TODO: Game over start here.
            }
        }
        else if (WonBattle)
        {
            _formation.Kill();

            _gameContext.GameObjects.BattleEndRequest = new BattleEndRequest() { Script = _formation.OnWinScript };
        }
    }

    private int GetNumberOfOptions()
    {
        var currentCharacter = _party.Characters[_currentCharacterIndex];
        var numberOfOptions = currentCharacter.AbilityIDs.Count + currentCharacter.AbilitySetIDs.Count;
        if (_itemsEnabled)
        {
            numberOfOptions ++;
        }
        if (_runAwayEnabled)
        {
            numberOfOptions ++;
        }
        if (_maxChoicesPerPage > numberOfOptions)
        {
            return numberOfOptions - 1;
        }
        return _maxChoicesPerPage - 1;

    } 
}