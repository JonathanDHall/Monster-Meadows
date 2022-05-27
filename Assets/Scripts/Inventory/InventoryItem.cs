using System;

[Serializable]
public class InventoryItem
{
    public InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }

    public InventoryItem(InventoryItemData source, int stackSize = 1)
    {
        data = source;
        AddToStack(stackSize);
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}