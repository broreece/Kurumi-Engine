namespace Data.Runtime.Entities.Base;

public interface IStats 
{
    public int CurrentHP { get; set; }

    public int GetMaxHp();

    public IReadOnlyList<int> GetStats();
}