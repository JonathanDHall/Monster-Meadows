using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Dictionary<InventoryItemData, InventoryItem> itemDictionary;
    public List<InventoryItem> inventory { get; private set; }

    public static InventorySystem _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
        
        inventory = new List<InventoryItem>();
        itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
    }

    public bool CheckIfInventoryContains(InventoryItemData reference)
    {
        foreach (var item in inventory)
        {
            if (item.data.id == reference.id)
            {
                return true;
            }
        }

        return false;
        //InventoryItem newItem = new InventoryItem(reference);

        //if (inventory.Contains(newItem))
        //    return true;
        //else
        //    return false;
    }

    public void Add (InventoryItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            itemDictionary.Add(referenceData, newItem);
        }
    }

    public void Remove(InventoryItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize <= 0)
            {
                inventory.Remove(value);

                itemDictionary.Remove(referenceData);
            }
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Remove(newItem);
            itemDictionary.Remove(referenceData);
        }
    }
}