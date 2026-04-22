namespace Data.Models.Characters;

public sealed class CharacterModel 
{
    public required int CurrentHP { get; set; }
    public required int MaxHP { get; set; }
    public required int CurrentMP { get; set; }
    public required int MaxMP { get; set; }

    public required List<int> Abilities { get; set; } = [];
    public required List<int> EquipmentTypes { get; set; } = [];
    public required List<int> Statuses { get; set; } = [];
    
    public required Dictionary<int, List<int>> AbilitySets { get; set; } = [];

    public required int[] ElementResistances { get; set; } = [];
    public required int[] Equipped { get; set; } = [];
    public required int[] Stats { get; set; } = [];
    public required int[] StatusResistances { get; set; } = [];
}