namespace Data.Definitions.Entities.Statuses.Base;

public enum StatusPriority 
{
    CanNotStackLowPriority = 0,
    CanStack = 1,
    CanNotStackHighPriority = 2,
    Fainted = 3
}