using Data.Definitions.Maps.Base;
using Engine.Systems.Navigation.Core;

namespace Engine.Systems.Pathfinding;

/// <summary>
/// Service used to utilize A star search to best find a path from an origin to a target on a provided navigation grid.
/// </summary>
public sealed class AStarSearch 
{
    public int LoadFastestPath(int originX, int originY, int targetX, int targetY, NavigationGrid navigationGrid) 
    {
        var openSet = new PriorityQueue<AStarNode, int>();
        var closedSet = new HashSet<(int, int)>();
        
        // Create first node in queue.
        var originTileNode = new AStarNode() 
        { 
            Parent = null, 
            XLocation = originX, 
            YLocation = originY, 
            DistanceFromStart = 0, 
            DistanceFromGoal = FastestPathHeuristic(originX, originY, targetX, targetY) 
        };
        openSet.Enqueue(originTileNode, originTileNode.TotalDistance);

        // Loop untill queue is empty.
        while (openSet.Count > 0) 
        {
            var currentNode = openSet.Dequeue();
            var currentX = currentNode.XLocation;
            var currentY = currentNode.YLocation;
            if (FoundTarget(currentX, currentY, targetX, targetY)) 
            {
                // Load parent node untill we get the base move.
                while (currentNode.Parent != null && currentNode.Parent.Parent != null) 
                {
                    currentNode = currentNode.Parent;
                }

                // Check the direction being moved in and then convert that into the enum format.
                var rightDirection = currentNode.XLocation - originX;
                var downDirection = currentNode.YLocation - originY;
                var direction = rightDirection == -1 ? (int) Direction.West : 
                    rightDirection == 1 ? (int) Direction.East : 
                    downDirection == -1 ? (int) Direction.North : 
                    (int) Direction.South;
                return direction;
            }

            // If the current node isn't the target add it to the closed queue.
            closedSet.Add((currentX, currentY));

            // Loop for each direction and add to open set if the tile is passable.
            for (var direction = 0; direction < 4; direction ++) 
            {
                // Get both x movement and y movement based on direction being checked.
                var rightMovement = direction % 2 == 0 ? 0 : (direction % 4 == 1 ? 1 : -1);
                var downMovement = direction % 2 == 1 ? 0 : (direction == 0 ? -1 : 1);
                if (navigationGrid.IsNavigable(currentX + rightMovement, currentY + downMovement)) 
                {
                    // Create a new node and place on queue.
                    var currentDistance = currentNode.DistanceFromStart + 1;
                    var newX = currentX + rightMovement;
                    var newY = currentY + downMovement;
                    AStarNode newNode = new() 
                    { 
                        Parent = currentNode, 
                        XLocation = newX, 
                        YLocation = newY, 
                        DistanceFromStart = currentDistance, 
                        DistanceFromGoal = FastestPathHeuristic(newX, newY, targetX, targetY) 
                    };
                    openSet.Enqueue(newNode, newNode.TotalDistance);
                }
            }
        }
        // If no move possible return -1.
        return -1;
    }

    private bool FoundTarget(int currentX, int currentY, int targetX, int targetY) 
    {
        return (currentX == targetX && Math.Abs(currentY - targetY) == 1) ||
            (currentY == targetY && Math.Abs(currentX - targetX) == 1);
    }

    private int FastestPathHeuristic(int originX, int originY, int targetX, int targetY) 
    {
        return Math.Abs(originX - targetX) + Math.Abs(originY - targetY);
    }

    private sealed class AStarNode 
    {
        public AStarNode ? Parent { get; init; }

        public int XLocation { get; init; }
        public int YLocation { get; init; }
        public int DistanceFromStart { get; init; }
        public int DistanceFromGoal { get; init; }

        public int TotalDistance => DistanceFromStart + DistanceFromGoal;
    }
}