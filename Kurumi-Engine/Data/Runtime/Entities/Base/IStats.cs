namespace Data.Runtime.Entities.Base;

public interface IStats 
{
    public int CurrentHP { get; set; }

    public int MaxHp { get; }

    public IReadOnlyList<int> Stats { get; }
}