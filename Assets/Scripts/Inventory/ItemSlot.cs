using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private GameObject _stackObj;
    [SerializeField] private TextMeshProUGUI _stackLabel;

    public void Set(InventoryItem item)
    {
        //_icon.sprite = item.data.icon;
        _label.text = item.data.displayName;

        if (item.stackSize <= 1)
        {
            _stackObj.SetActive(false);
            return;
        }

        _stackLabel.text = item.stackSize.ToString();
    }
}