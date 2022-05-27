using UnityEngine;

public class QuestItem : MonoBehaviour
{
    private Collection _collection;
    [SerializeField] private bool _destroyOnLoad;
    [SerializeField] public InventoryItemData ItemName;

    void Start()
    {
        _collection = Collection._instance;

        if (_destroyOnLoad)
        {
            if (InventorySystem._instance.CheckIfInventoryContains(ItemName))
            {
                gameObject.SetActive(false);
                //Destroy(this.gameObject);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GetComponent<ItemObject>().OnHandlePickUpItem();
            AddToQuestItem();
        }
    }

    public void AddToQuestItem(bool delay = false)
    {
        StringPopUp.Create(("Picked up " + ItemName.displayName + "!"));
        //InventorySystem._instance.Add(ItemName);
        if (delay)
            Invoke("DelayDestroy", 1);
        else
            Destroy(this.gameObject);
    }
}