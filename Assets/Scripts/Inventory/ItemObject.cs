using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    public void OnHandlePickUpItem()
    {
        InventorySystem._instance.Add(referenceItem);
        Destroy(gameObject);
    }
}