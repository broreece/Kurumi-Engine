namespace Game.Map.ActorControllers;

/// <summary>
/// The random actor controller.
/// </summary>
public sealed class RandomActorController : ActorController {
    /// <summary>
    /// Constructor for the random actor controller.
    /// </summary>
    /// /// <param name="interval">The interval at which the actor moves.</param>
    /// <param name="xLocation">The stored x location of the actor.</param>
    /// <param name="yLocation">The stored y location of the actor.</param>
    public RandomActorController(int interval, int xLocation, int yLocation) 
        : base(interval, xLocation, yLocation) {
        fixedActorInterval = interval;
    }

    /// <summary>
    /// Function used to generate a random move.
    /// </summary>
    /// <returns>A random direction.</returns>
    public override int GetMove() {
        // Generate the random move.
        Random rand = new();
        int randomMove = rand.Next(0, 4);

        // Reset the interval to be based on the inital interval.
        interval = fixedActorInterval;

        duration = 0;
        return randomMove;
    }

    private readonly int fixedActorInterval;
}