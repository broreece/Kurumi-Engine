namespace Config.Runtime.Menus;

public sealed class InventoryConfig 
{
    public required int WindowId { get; init; }
    public required int ChoiceBoxId { get; init; }
    public required int FontId { get; init; }
    public required int FontSize { get; init; }

    public required int InventoryItemsWidth { get; init; }
    public required int InventoryItemsHeight { get; init; }
    public required int InventoryItemsX { get; init; }
    public required int InventoryItemsY { get; init; }
    
    public required int InventoryItemDescriptionWidth { get; init; }
    public required int InventoryItemDescriptionHeight { get; init; }
    public required int InventoryItemDescriptionX { get; init; }
    public required int InventoryItemDescriptionY { get; init; }
}