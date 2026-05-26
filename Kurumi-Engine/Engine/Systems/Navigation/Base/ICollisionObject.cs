namespace Engine.Systems.Navigation.Base;

public interface ICollisionObject
{
    public bool Passable { get; }

    public bool IsSeeThrough();
}