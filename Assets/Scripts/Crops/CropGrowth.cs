using UnityEngine;

public class CropGrowth : MonoBehaviour
{
    private CropPlot _manager;

    [Header("Crop Details")]
    [SerializeField] private string _cropName;
    [SerializeField] public int _seedTypeInt;
    [SerializeField] private int _harvestAmount;

    [Header("Phase Details")]
    public int _curPhase;
    [SerializeField] private int _daysBetweenPhases;
    public int _dayCount;
    [SerializeField] private GameObject[] _phases;
    public bool _isGrowning = true;
    [SerializeField] private GameObject _interactUI;

    [Header("Watering Details")]
    public bool _isWatered;
    [SerializeField] private GameObject _interactUIWater;

    public string ID { get; private set; }

    void Start()
    {
        GameEvents.NewDay += Grow;
        _manager = GetComponentInParent<CropPlot>();
        ID = _manager.ID;
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
                    _manager.Save();
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
                    _manager.Save();
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
        {
            _isWatered = true;
            _isGrowning = false;
        }

        _manager.Save();
    }

    public void SetUp(Collection.CropData data)
    {
        _manager = GetComponentInParent<CropPlot>();
        _curPhase = data.m_curPhase;
        _dayCount = data.m_dayCount;
        _isGrowning = data.m_isGrowningg;
        _isWatered = data.m_isWatered;

        _phases[0].SetActive(false);
        _phases[_curPhase].SetActive(true);
    }
}