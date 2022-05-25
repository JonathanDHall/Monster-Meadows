using UnityEngine;

public class CropPlot : MonoBehaviour
{
    [Header("Plant Spawning")]
    [SerializeField] private GameObject _plantSpawnPoint;
    public enum SeedType { CropGiantMushroom, CropWhiteFlower, }
    public SeedType _typeToPlant;
    [SerializeField] private GameObject _interactUI;
    private GameObject _CurPlant;
    [SerializeField] private GameObject _plantSelectUI;

    [Header("Dirt")]
    [SerializeField] private Renderer _dirt;
    [SerializeField] private Material _dryDirt;
    [SerializeField] private Material _wateredDirt;
    
    public void WaterDirt(bool _water)
    {
        switch (_water)
        {
            case true:
                _dirt.material = _wateredDirt;
                break;

            case false:
                _dirt.material = _dryDirt;
                break;
        }
    }

    public void SelectPlant()
    {
        var temp =  Instantiate(Resources.Load("Crops/" + _typeToPlant.ToString()), _plantSpawnPoint.transform);
        _CurPlant = (GameObject)temp;
    }

    public void SetSeed(int seed)
    {
        _typeToPlant = (SeedType)seed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        if (_CurPlant == null)
            _interactUI.SetActive(true);

    }

    private void Update()
    {
        if (_CurPlant == null)
        {
            if (_interactUI.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _interactUI.SetActive(false);
                    _plantSelectUI.SetActive(true);
                    PlayerController._instance.DisableControls();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        if (_CurPlant == null)
            _interactUI.SetActive(false);
    }

    public void OnExit()
    {
        PlayerController._instance.EnableControls();
    }
}