using UnityEngine;

public class UniqueID : MonoBehaviour
{
    public string ID { get; private set; }

    [SerializeField] public string ItemResourceName;
    public string ItemID { get; set; }

    private Collection _collection;
    [SerializeField] private bool _destroyOnLoad;
    [SerializeField] private bool _customID;
    [SerializeField] private string CustomID;

    private void Awake()
    {
        ItemID = ItemResourceName;
        if (_customID)
            ID = CustomID;
        else
            ID = transform.position.sqrMagnitude + "_" + transform.rotation.eulerAngles.sqrMagnitude + "_" + name + "_" + transform.GetSiblingIndex();
    }

    private void Start()
    {
        _collection = Collection._instance;

        if (_destroyOnLoad)
        {
            if (_collection.Trash.Contains(ID))
            {
                Destroy(this.gameObject);
                return;
            }
        }
    }

    public void AddToTrash(bool delay)
    {
        _collection.Trash.Add(ID);
        if (delay)
            Invoke("DelayDestroy", 1);
        else
            Destroy(this.gameObject);
    }

    public void DelayDestroy()
    {
        Destroy(this.gameObject);
    }
}