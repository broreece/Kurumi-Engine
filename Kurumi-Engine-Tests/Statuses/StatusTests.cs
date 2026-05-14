namespace Tests.Statuses;

using Data.Definitions.Entities.Factories;
using Data.Definitions.Entities.Statuses.Base;
using Data.Definitions.Entities.Statuses.Factories;
using Data.Models.Formations;
using Data.Runtime.Entities.Factories;
using Data.Runtime.Entities.Statuses.Core;
using Data.Runtime.Entities.Statuses.Factories;

using Engine.Systems.Statuses.Core;

using Xunit;

public class StatusTests 
{
    [Fact]
    public void StatusApplicationTest() 
    {
        var canNotStackLowPriority = (int) StatusPriority.CanNotStackLowPriority;
        var canStack = (int) StatusPriority.CanStack;
        var canNotStackHighPriority = (int) StatusPriority.CanNotStackHighPriority;
        var fainted = (int) StatusPriority.Fainted;

        var statusDefinitionFactory = new StatusDefinitionFactory();
        var status1 = statusDefinitionFactory.Create(1, 5, canNotStackLowPriority, 0, 0, "test", null, "test", "test", 
            false, false, new Dictionary<int, int>(), new Dictionary<int, int>(), [], [], []);
        var status2 = statusDefinitionFactory.Create(2, 5, canStack, 0, 0, "test", null, "test", "test", 
            false, false, new Dictionary<int, int>(), new Dictionary<int, int>(), [], [], []);
        var status3 = statusDefinitionFactory.Create(3, 5, canNotStackHighPriority, 0, 0, "test", null, "test", "test", 
            false, false, new Dictionary<int, int>(), new Dictionary<int, int>(), [], [], []);
        var status4 = statusDefinitionFactory.Create(4, 5, fainted, 0, 0, "test", null, "test", "test", 
            false, false, new Dictionary<int, int>(), new Dictionary<int, int>(), [], [], []);
        var statusFactory = new StatusFactory();
        IReadOnlyList<Status> statuses = [ statusFactory.Create(status1), statusFactory.Create(status2), 
            statusFactory.Create(status3), statusFactory.Create(status4) ];

        var statusResolver = new StatusResolver();
        
        var enemyModel = new EnemyModel() { Id = 1, Statuses = [] };

        var entityDefinitionFactory = new EntityDefinitionFactory();
        var entityDefinition = entityDefinitionFactory.Create(1, 10, "test", "test", "test", [], [], [],
            []);

        var entityFactory = new EntityFactory();
        var entity = entityFactory.Create(entityDefinition, enemyModel, 10);

        statusResolver.TryApplyStatus(entity, statuses[0]);
        Assert.Single(entity.GetStatuses());
        Assert.Contains(statuses[0], entity.GetStatuses());

        statusResolver.TryApplyStatus(entity, statuses[2]);
        Assert.Single(entity.GetStatuses());
        Assert.DoesNotContain(statuses[0], entity.GetStatuses());
        Assert.Contains(statuses[2], entity.GetStatuses());
    }
}