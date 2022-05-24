using UnityEngine;

public class QuestItem : MonoBehaviour
{
    private Collection _collection;
    [SerializeField] private bool _destroyOnLoad;
    [SerializeField] private string ItemName;

    void Start()
    {
        _collection = Collection._instance;

        if (_destroyOnLoad)
        {
            if (_collection.QuestItems.Contains(ItemName))
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
            AddToQuestItem();
        }
    }

    public void AddToQuestItem(bool delay = false)
    {
        StringPopUp.Create(("Picked up " + ItemName + "!"));
        _collection.QuestItems.Add(ItemName);
        if (delay)
            Invoke("DelayDestroy", 1);
        else
            Destroy(this.gameObject);
    }
}