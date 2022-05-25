using UnityEngine;

public class CropGrowth : MonoBehaviour
{
    [Header("Crop Details")]
    [SerializeField] private string _cropName;
    [SerializeField] private int _harvestAmount;

    [Header("Phase Details")]
    private int _curPhase;
    [SerializeField] private GameObject[] _phases;
    private bool _isGrowning = true;
    private bool _isWatered = true;

    [SerializeField] private GameObject _interactUI;

    void Start()
    {
        GameEvents.NewDay += Grow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isGrowning)
            return;

        if (!other.transform.CompareTag("Player"))
            return;

        _interactUI.SetActive(true);
    }

    private void Update()
    {
        if (_isGrowning)
            return;

        if (_interactUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _interactUI.SetActive(false);

                StringPopUp.Create("Harvested " + _harvestAmount.ToString() + "X " + _cropName + "!");

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isGrowning)
            return;

        if (!other.transform.CompareTag("Player"))
            return;

        _interactUI.SetActive(false);
    }

    void Grow()
    {
        if (!_isGrowning || !_isWatered)
            return;

        _phases[_curPhase].SetActive(false);
        _curPhase++;
        _phases[_curPhase].SetActive(true);

        //ToDo: Once watering is implemented, set _isWatered to false

        if (_curPhase + 1 >= _phases.Length)
            _isGrowning = false;
    }
}