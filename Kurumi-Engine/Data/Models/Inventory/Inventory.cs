namespace Data.Models.Inventory;

public sealed class Inventory
{
    public required Dictionary<int, int> Items { get; set; }

    public void IncrementAmount(int itemId)
    {
        if (Items.TryGetValue(itemId, out int currentAmount))
        {
            Items[itemId] = currentAmount + 1;
        }
        else
        {
            Items[itemId] = 1;
        }
    }

    public void RemoveAmountOfItem(int itemId, int amount)
    {
        if (Items.TryGetValue(itemId, out int currentAmount))
        {
            if (currentAmount <= amount)
            {
                Items.Remove(itemId);
            }
            else
            {
                Items[itemId] = currentAmount - amount;
            }
        }
    }

    public int GetAmountOf(int itemId)
    {
        if (Items.TryGetValue(itemId, out int currentAmount))
        {
            return currentAmount;
        }
        else
        {
            return 0;
        }
    }
}