namespace Game.Map.Pathfinding;

/// <summary>
/// The A star search isolated class used by the map scene.
/// </summary>
public static class AStarSearch {
    /// <summary>
    /// A node used in the A star search for smart enemy tracking.
    /// </summary>
    public class AStarNode {
        /// <summary>
        /// Constructor for the A star search node.
        /// </summary>
        /// <param name="xLocation">The x location of the node.</param>
        /// <param name="yLocation">The y location of the node.</param>
        /// <param name="distanceFromStart">The distance traveled from the start.</param>
        /// <param name="distanceFromGoal">The distance left from the goal.</param>
        /// <param name="parent">The parent (if the node has one).</param>
        public AStarNode(int xLocation, int yLocation, int distanceFromStart, int distanceFromGoal, AStarNode ? parent) {
            XLocation = xLocation;
            YLocation = yLocation;
            DistanceFromStart = distanceFromStart;
            DistanceFromGoal = distanceFromGoal;
            this.parent = parent;
        }

        public int XLocation { get; }
        public int YLocation { get; }
        public int DistanceFromStart { get; }
        private int DistanceFromGoal { get; }

        // Total distance, an estimated total distance of a path based on it's distance from start and distance from goal.
        public int TotalDistance => DistanceFromStart + DistanceFromGoal;

        public AStarNode ? parent;
    }

    /// <summary>
    /// A function that uses A* search algorithm to load the fastest path using a target point and origin point.
    /// </summary>
    /// <param name="originX">The original X point.</param>
    /// <param name="originY">The original Y point.</param>
    /// <param name="targetX">The target X point.</param>
    /// <param name="targetY">The target Y point.</param>
    /// <returns>The next optimal direction to move in.</returns>
    public static int LoadFastestPath(int originX, int originY, int targetX, int targetY, INavigationGrid navigationGrid) {
        PriorityQueue<AStarNode, int> openSet = new();
        HashSet<(int, int)> closedSet = [];
        
        // Create first node in queue.
        AStarNode originTileNode = new(originX, originY, 0, FastestPathHeuristic(originX, originY, targetX, targetY), null);
        openSet.Enqueue(originTileNode, originTileNode.TotalDistance);

        // Loop untill queue is empty.
        while (openSet.Count > 0) {
            AStarNode currentNode = openSet.Dequeue();
            int currentX = currentNode.XLocation;
            int currentY = currentNode.YLocation;
            if (currentX == targetX && currentY == targetY) {
                // Load parent node untill we get the base move.
                while (currentNode.parent != null && currentNode.parent.parent != null) {
                    currentNode = currentNode.parent;
                }

                // Check the direction being moved in and then convert that into the enum format.
                int rightDirection = currentNode.XLocation - originX;
                int downDirection = currentNode.YLocation - originY;
                int direction = rightDirection == -1 ? 3 : rightDirection == 1 ? 1 : downDirection == -1 ? 0 : 2;
                return direction;
            }

            // If the current node isn't the target add it to the closed queue.
            closedSet.Add((currentX, currentY));

            // Loop for each direction and add to open set if the tile is passable.
            for (int direction = 0; direction < 4; direction ++) {
                // Get both x movement and y movement based on direction being checked.
                int rightMovement = direction % 2 == 0 ? 0 : (direction % 4 == 1 ? 1 : -1);
                int downMovement = direction % 2 == 1 ? 0 : (direction == 0 ? -1 : 1);
                if (navigationGrid.IsNavigable(currentX + rightMovement, currentY + downMovement)) {
                    // Create a new node and place on queue.
                    int currentDistance = currentNode.DistanceFromStart + 1;
                    int newX = currentX + rightMovement;
                    int newY = currentY + downMovement;
                    AStarNode newNode = new(newX, newY, currentDistance, FastestPathHeuristic(newX, newY, targetX, targetY), currentNode);
                    openSet.Enqueue(newNode, newNode.TotalDistance);
                }
            }
        }
        // If no move possible return -1.
        return -1;
    }

    /// <summary>
    /// Helper function used in the A* search to find the Heuristic value of the current path.
    /// </summary>
    /// <param name="originX">The original X point.</param>
    /// <param name="originY">The original Y point.</param>
    /// <param name="targetX">The target X point.</param>
    /// <param name="targetY">The target Y point.</param>
    /// <returns>An integer value representing how "valuable" a path is currently.</returns>
    private static int FastestPathHeuristic(int originX, int originY, int targetX, int targetY) {
        return Math.Abs(originX - targetX) + Math.Abs(originY - targetY);
    }
}