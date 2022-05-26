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

    public string ID { get; private set; }

    private void Awake()
    {
        ID = transform.position.sqrMagnitude + "_" + transform.rotation.eulerAngles.sqrMagnitude;
        GameEvents.SaveInitiated += Save;
        //GameEvents.LoadInitiated += Load;
    }

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        if (_CurPlant != null)
        {
            Collection.CropData cropData = new Collection.CropData();
            var data = _CurPlant.GetComponent<CropGrowth>();
            cropData.ID = ID;
            cropData.m_curPhase = data._curPhase;
            cropData.m_dayCount = data._dayCount;
            cropData.m_isGrowningg = data._isGrowning;
            cropData.m_isWatered = data._isWatered;
            cropData.m_seedTypeInt = data._seedTypeInt;

            for (int i = 0; i < Collection._instance.CropList.Count; i++)
            {
                if (Collection._instance.CropList[i].ID == ID)
                {
                    Collection._instance.CropList.Remove(Collection._instance.CropList[i]);
                }
            }

            Collection._instance.CropList.Add(cropData);
            //SaveSystem.Save(_CurPlant.GetComponent<CropGrowth>(), ID);
        }
        else
        {
            for (int i = 0; i < Collection._instance.CropList.Count; i++)
            {
                if (Collection._instance.CropList[i].ID == ID)
                {
                    Collection._instance.CropList.Remove(Collection._instance.CropList[i]);
                }
            }
        }
    }

    void Load()
    {
        foreach (var item in Collection._instance.CropList)
        {
            if (item.ID == ID)
            {
                _typeToPlant = (SeedType)item.m_seedTypeInt;
                var newPlant = Instantiate(Resources.Load("Crops/" + _typeToPlant.ToString()), _plantSpawnPoint.transform);
                _CurPlant = (GameObject)newPlant;
                var plantCropGrowth = _CurPlant.GetComponent<CropGrowth>();
                WaterDirt(item.m_isWatered);

                plantCropGrowth.SetUp(item);
            }
        }

        //if (SaveSystem.SaveExists(ID))
        //{
        //    CropGrowth baseValues = SaveSystem.Load<CropGrowth>(ID);
        //    SetSeed(baseValues._seedTypeInt);
        //    var newPlant = Instantiate(Resources.Load("Crops/" + _typeToPlant.ToString()), _plantSpawnPoint.transform);
        //    _CurPlant = (GameObject)newPlant;
        //    var plantCropGrowth = _CurPlant.GetComponent<CropGrowth>();

        //    plantCropGrowth.SetUp(baseValues);
        //}
    }

    public void WaterDirt(bool _water)
    {
        if (_dirt == null)
            _dirt = GetComponentInChildren<MeshRenderer>();

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