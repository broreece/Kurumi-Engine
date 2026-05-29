// Data.
using Data.Definitions.Actors.Core;
using Data.Definitions.Formations.Core;

using Data.Models.Formations;

using Data.Runtime.Entities.Core;
using Data.Runtime.Formations.Base;
using Data.Runtime.Maps.Base.Controllers.Base;
using Data.Runtime.Spatials;

// Engine.
using Engine.State.States.Maps.Base;

using Engine.Systems.Navigation.Base;

namespace Data.Runtime.Formations.Core;

public sealed class Formation : IFacingPositionProvider, IMapEntity, IMutablePositionProvider, ICollisionObject 
{
    private readonly FormationDefinition _formationDefinition;
    private readonly FormationModel _formationModel;

    // Map elements.
    // The actor information of the formation.
    private readonly ActorInfo _defaultActor;
    private readonly ActorInfo _onFoundActor;
    private readonly Controller? _defaultController;
    private readonly Controller? _onFoundController;

    // Battle elements.
    // List of entities which contains the stats/statuses of the formation.
    private readonly IReadOnlyList<Entity> _entities;

    // Alert timer of the formation.
    private float _alertTimer;

    // All formations are not passable to avoid infinite battle issues.
    public bool Passable => false;

    // The current state of the formation.
    public bool Alert { get; set; } = false;

    // List of enemies which contains the drawable location and battle scripts of the formation.
    public IReadOnlyList<Enemy> Enemies { get; }

    // Walk animation variables.
    public bool IsMoving { get; set; } = false;
    public int WalkAnimationFrame { get; set; } = 0;
    public int LastX { get; set; }
    public int LastY { get; set; }
    public float AnimationTimer { get; set; } = 0;
    public float MovementProgress { get; set; } = 1;

    // IMutablePositionProvider and IFacingPositionProvider functionality.
    public int XLocation 
    {
        get => _formationModel.XLocation;
        set => _formationModel.XLocation = value;
    }

    public int YLocation 
    {
        get => _formationModel.YLocation;
        set => _formationModel.YLocation = value;
    }

    public int Facing 
    {
        get => _formationModel.Facing;
        set => _formationModel.Facing = value;
    }

    public bool AlertLimitReached => _alertTimer >= _formationDefinition.SearchTimer;

    public bool HasOnLoseScript => _formationDefinition.OnLoseScript != null;

    public required IReadOnlyList<StoredEntityData> StoredEntityData { get; init; }

    internal Formation(
        FormationDefinition formationDefinition, 
        FormationModel formationModel, 
        ActorInfo defaultActor, 
        ActorInfo onFoundActor, 
        Controller? defaultController, 
        Controller? onFoundController, 
        IReadOnlyList<Entity> entities, 
        IReadOnlyList<Enemy> enemies
    ) 
    {
        _formationDefinition = formationDefinition;
        _formationModel = formationModel;

        _defaultActor = defaultActor;
        _onFoundActor = onFoundActor;
        _defaultController = defaultController;
        _onFoundController = onFoundController;

        _entities = entities;
        Enemies = enemies;
    }

    // Tracking range.
    public int GetTrackingRange() => Alert ? _onFoundActor.TrackingRange : _defaultActor.TrackingRange;

    // Actor movement functions.
    public int GetSpriteId() => Alert ? _onFoundActor.SpriteId : _defaultActor.SpriteId;

    public bool BelowParty => Alert ? _onFoundActor.BelowParty : _defaultActor.BelowParty;

    public bool SeeThrough => Alert ? _onFoundActor.SeeThrough : _defaultActor.SeeThrough;

    public int GetMovementSpeed() => Alert ? _onFoundActor.MovementSpeed : _defaultActor.MovementSpeed;

    public void StartMovement() 
    {
        WalkAnimationFrame = 1;
        MovementProgress = 0;
        IsMoving = true;
    }

    public void ResetAlertTimer() => _alertTimer = 0;

    public void Update(float deltaTime) => _alertTimer += deltaTime;

    // Battle functions.
    public bool IsDefeated()
    {
        foreach (var entityData in StoredEntityData)
        {
            if (entityData.Entity.CurrentHP > 0)
            {
                return false;
            }
        }
        return true;
    }

    public int GetAmountOfLivingEnemies()
    {
        var size = 0;
        foreach (var enemy in _entities)
        {
            if (enemy.CurrentHP > 0)
            {
                size ++;
            }
        }
        return size;
    }

    public Entity GetEntityAt(int entityIndex) => _entities[entityIndex];

    public Controller? GetCurrentController() => Alert ? _onFoundController : _defaultController;
}
