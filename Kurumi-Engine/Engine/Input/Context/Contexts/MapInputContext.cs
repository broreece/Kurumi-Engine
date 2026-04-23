using Data.Runtime.Party.Core;

using Engine.Input.Base;
using Engine.Input.Context.Base;
using Engine.Systems.Movement.Core;

namespace Engine.Input.Context.Contexts;

/// <summary>
/// Utilizes a movement resolver per map to handle party movements.
/// </summary>
public class MapInputContext : IGameplayInputContext 
{
    private readonly Party _party;

    private MovementResolver _movementResolver;

    public bool InteractRequested { get; private set; } = false;

    public MapInputContext(Party party, MovementResolver movementResolver) 
    {
        _party = party;
        _movementResolver = movementResolver;
    }

    public void Handle(InputState input) 
    {
        if (input.IsPressed(InputAction.MoveUp) && !_party.IsMoving) 
        {
            _movementResolver.TryMove(_party, direction: 0);
        }
        if (input.IsPressed(InputAction.MoveRight) && !_party.IsMoving) 
        {
            _movementResolver.TryMove(_party, direction: 1);
        }
        if (input.IsPressed(InputAction.MoveDown) && !_party.IsMoving) 
        {
            _movementResolver.TryMove(_party, direction: 2);
        }
        if (input.IsPressed(InputAction.MoveLeft) && !_party.IsMoving) 
        {
            _movementResolver.TryMove(_party, direction: 3);
        }
        // If confirm is pressed, handle action at next possible moment.
        InteractRequested = input.IsPressed(InputAction.Confirm);
    }

    // Allows updating of the map without creation of new map input context with the party each time.
    public void SetMovementResolver(MovementResolver movementResolver) => _movementResolver = movementResolver;
}