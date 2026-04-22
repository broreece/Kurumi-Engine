namespace Tests.Statuses;

using Data.Definitions.Entities.Factories;
using Data.Definitions.Entities.Status.Base;
using Data.Definitions.Entities.Status.Core;
using Data.Definitions.Entities.Status.Factories;
using Data.Models.Formations;
using Data.Runtime.Entities.Factories;
using Engine.Systems.Statuses.Core;
using Infrastructure.Database.Base;
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

        var statusFactory = new StatusFactory();
        var status1 = statusFactory.Create(1, 5, canNotStackLowPriority, 0, 0, "test", null, "test", "test", 
            false, false, new Dictionary<int, int>(), new Dictionary<int, int>(), [], [], []);
        var status2 = statusFactory.Create(2, 5, canStack, 0, 0, "test", null, "test", "test", 
            false, false, new Dictionary<int, int>(), new Dictionary<int, int>(), [], [], []);
        var status3 = statusFactory.Create(3, 5, canNotStackHighPriority, 0, 0, "test", null, "test", "test", 
            false, false, new Dictionary<int, int>(), new Dictionary<int, int>(), [], [], []);
        var status4 = statusFactory.Create(4, 5, fainted, 0, 0, "test", null, "test", "test", 
            false, false, new Dictionary<int, int>(), new Dictionary<int, int>(), [], [], []);
        IReadOnlyList<Status> statuses = [ status1, status2, status3, status4 ];

        var statusRegistry = new Registry<Status>(statuses, status => status.Id);
        var statusResolver = new StatusResolver(statusRegistry);
        
        var enemyModel = new EnemyModel() { Id = 1, Statuses = [] };

        var entityDefinitionFactory = new EntityDefinitionFactory();
        var entityDefinition = entityDefinitionFactory.Create(1, 10, "test", "test", "test", [], [], [],
            []);

        var entityFactory = new EntityFactory();
        var entity = entityFactory.Create(entityDefinition, enemyModel, 10);

        statusResolver.ApplyStatus(entity, 1);
        Assert.Single(entity.GetStatuses());
        Assert.Contains(status1.Id, entity.GetStatuses());

        statusResolver.ApplyStatus(entity, 3);
        Assert.Single(entity.GetStatuses());
        Assert.DoesNotContain(status1.Id, entity.GetStatuses());
        Assert.Contains(status3.Id, entity.GetStatuses());
    }
}