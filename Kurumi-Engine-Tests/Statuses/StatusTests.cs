namespace Tests.Statuses;

using Engine.Systems;
using Game.Entities.Enemy;
using Game.Entities.Status;
using Xunit;

/// <summary>
/// The status tests class.
/// </summary>
public class StatusTests {
    /// <summary>
    /// Test to check that when adding a status correct application rules apply.
    /// </summary>
    [Fact]
    public void StatusApplicationTest() {
        Enemy testEntity = new("Test name", "Test description", 10, 0, [0], [0], [0], []);
        Status testStatus = new("Test name", "Test description", 0, 0, 0, 0, 0, 0, 0, false, false, [0], [0], "", [], [], []);
        Status higherPriorityStatus = new("Test name 2", "Test description", 0, 0, 0, 0, 1, 0, 0, false, false, [0], [0], "", [], [], []);
        Status stackableStatus = new("Test name 3", "Test description", 0, 0, 0, 0, 1, 0, 0, false, false, [0], [0], "", [], [], []);
        Status highestPriorityStatus = new("Test name 4", "Test description", 0, 0, 0, 0, 2, 0, 0, false, false, [0], [0], "", [], [], []);
        StatusResolver statusResolver = new();
        statusResolver.ApplyStatus(testEntity, testStatus);
        
        // Test applying a single 0 priority status works.
        List<Status> statuses = testEntity.GetStatuses();
        Assert.Single(statuses);
        Assert.Contains(testStatus, statuses);

        // Test applying stackable statuses work.
        statusResolver.ApplyStatus(testEntity, higherPriorityStatus);
        statusResolver.ApplyStatus(testEntity, stackableStatus);
        Assert.DoesNotContain(testStatus, statuses);
        Assert.Contains(higherPriorityStatus, statuses);
        Assert.Contains(stackableStatus, statuses);

        // Test highest priority.
        statusResolver.ApplyStatus(testEntity, highestPriorityStatus);
        List<Status> newStatuses = testEntity.GetStatuses();
        Assert.Single(newStatuses);
        Assert.Contains(highestPriorityStatus, newStatuses);
    }
}