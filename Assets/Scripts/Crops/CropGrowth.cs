using UnityEngine;

public class CropGrowth : MonoBehaviour
{
    private CropPlot _manager;

    [Header("Crop Details")]
    [SerializeField] private string _cropName;
    [SerializeField] private int _harvestAmount;

    [Header("Phase Details")]
    private int _curPhase;
    [SerializeField] private int _daysBetweenPhases;
    private int _dayCount;
    [SerializeField] private GameObject[] _phases;
    private bool _isGrowning = true;
    [SerializeField] private GameObject _interactUI;

    [Header("Watering Details")]
    private bool _isWatered;
    [SerializeField] private GameObject _interactUIWater;


    void Start()
    {
        GameEvents.NewDay += Grow;
        _manager = GetComponentInParent<CropPlot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        if (!_isWatered && _isGrowning)
            _interactUIWater.SetActive(true);

        if (!_isGrowning)
            _interactUI.SetActive(true);
    }

    private void Update()
    {
        if (!_isGrowning)
        {
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

        if (!_isWatered && _isGrowning) 
        {
            if (_interactUIWater.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _interactUIWater.SetActive(false);

                    StringPopUp.Create("Watered " + _cropName + ".");

                    _isWatered = true;
                    _manager.WaterDirt(_isWatered);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        if (!_isWatered && _isGrowning)
            _interactUIWater.SetActive(false);

        if (!_isGrowning)
            _interactUI.SetActive(false);
    }

    void Grow()
    {
        if (!_isGrowning || !_isWatered)
            return;

        _dayCount++;

        if (_dayCount >= _daysBetweenPhases)
        {
            _dayCount = 0;
            _phases[_curPhase].SetActive(false);
            _curPhase++;
            _phases[_curPhase].SetActive(true);
        }
        _isWatered = false;
        _manager.WaterDirt(_isWatered);

        if (_curPhase + 1 >= _phases.Length)
            _isGrowning = false;
    }
}