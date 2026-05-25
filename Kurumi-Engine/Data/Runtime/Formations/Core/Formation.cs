using Data.Definitions.Actors.Core;
using Data.Definitions.Formations.Core;

using Data.Models.Formations;

using Data.Runtime.Entities.Core;
using Data.Runtime.Formations.Base;
using Data.Runtime.Maps.Base.Controllers.Base;
using Data.Runtime.Spatials;

namespace Data.Runtime.Formations.Core;

public sealed class Formation : IMutablePositionProvider, IFacingPositionProvider, IWalkable 
{
    private readonly FormationDefinition _formationDefinition;
    private readonly FormationModel _formationModel;

    // Map elements.
    // The actor information of the formation.
    private readonly ActorInfo _defaultActor;
    private readonly ActorInfo _onFoundActor;

    // Battle elements.
    // List of entities which contains the stats/statuses of the formation.
    private readonly IReadOnlyList<Entity> _entities;

    // The current state of the formation.
    public required Controller CurrentController { get; init; }
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

    public required IReadOnlyList<StoredEntityData> StoredEntityData { get; init; }

    internal Formation(
        FormationDefinition formationDefinition, 
        FormationModel formationModel, 
        ActorInfo defaultActor, 
        ActorInfo onFoundActor, 
        IReadOnlyList<Entity> entities, 
        IReadOnlyList<Enemy> enemies
    ) 
    {
        _formationDefinition = formationDefinition;
        _formationModel = formationModel;

        _defaultActor = defaultActor;
        _onFoundActor = onFoundActor;

        _entities = entities;
        Enemies = enemies;
    }

    public void StartMovement() 
    {
        WalkAnimationFrame = 1;
        MovementProgress = 0;
        IsMoving = true;
    }

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
}
