using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;

    void Start()
    {
        InventorySystem._instance.onInventoryChangedEvent += OnUpdateInventory;
    }

    void OnUpdateInventory()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }

        DrawInventory();
    }

    void DrawInventory()
    {
        foreach (var item in InventorySystem._instance.inventory)
        {
            AddInventorySlot(item);
        }
    }

    void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(_slotPrefab);
        obj.transform.SetParent(transform, false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
}