using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    public string ID { get; private set; }

    private void Awake()
    {
        ID = transform.position.sqrMagnitude + "_" + transform.rotation.eulerAngles.sqrMagnitude;
    }

    private void Start()
    {
        if (Collection._instance.Trash.Contains(ID))
            Destroy(gameObject);
    }

    public void OnHandlePickUpItem()
    {
        InventorySystem._instance.Add(referenceItem);
        StringPopUp.Create(("Picked up " + referenceItem.displayName + "!"));
        Collection._instance.Trash.Add(ID);

        Destroy(gameObject);
    }
}