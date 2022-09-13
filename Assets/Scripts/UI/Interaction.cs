using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    [SerializeField] private GameObject _interactUI;
    private TextMeshProUGUI _text;
    [SerializeField] private LayerMask _mask;

    private void Start()
    {
        _text = _interactUI.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.1f, transform.TransformDirection(Vector3.forward), out hit, 3, _mask))
        {
            if (hit.transform.GetComponent<ItemObject>())
            {
                _interactUI.SetActive(true);
                EditText("Collect");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponent<ItemObject>().OnHandlePickUpItem();
                }
            }

            if (hit.transform.GetComponent<RootBridge>())
            {
                if (hit.transform.GetComponent<RootBridge>().curState == RootBridge.RootState.sprout)
                {
                    _interactUI.SetActive(true);
                    EditText("Water");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.transform.GetComponent<RootBridge>().AttemptToWater();
                    }
                }
            }

            if (hit.transform.GetComponent<CropPlot>())
            {
                var temp = hit.transform.GetComponent<CropPlot>();
                if (temp._CurPlant == null)
                {
                    if (!temp._plantSelectUI.activeInHierarchy)
                    {
                        _interactUI.SetActive(true);
                        EditText("Plant");

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            temp.ActvatePlantingUI();
                            _interactUI.SetActive(false);
                        }
                    }
                    return;
                }

                var temp2 = temp._CurPlant.GetComponent<CropGrowth>();

                if (temp2._isGrowning && !temp2._isWatered)
                {
                    _interactUI.SetActive(true);
                    EditText("Water");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        temp2.WaterCrop();
                        _interactUI.SetActive(false);
                    }
                    return;
                }
                else if (!temp2._isGrowning)
                {
                    _interactUI.SetActive(true);
                    EditText("Harvest");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        temp2.HarvestCrop();
                        _interactUI.SetActive(false);
                    }
                    return;
                }
            }
        }
        else
            _interactUI.SetActive(false);
    }

    void EditText(string text)
    {
        _text.text = "\"E\" To " + text + "!";
    }
}
