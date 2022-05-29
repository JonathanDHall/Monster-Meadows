using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropPlot : MonoBehaviour
{
    [Header("Plant Spawning")]
    [SerializeField] private GameObject _plantSpawnPoint;
    public enum SeedType { CropGiantMushroom, CropWhiteFlower, }
    public SeedType _typeToPlant;
    [SerializeField] private GameObject _interactUI;
    public GameObject _CurPlant;
    [SerializeField] public GameObject _plantSelectUI;
    [SerializeField] private Button _plantButton;
    [SerializeField] private InventoryItemData[] _seeds;
    //private List<InventoryItem> _seedsItems { get; set; }

    [Header("Dirt")]
    [SerializeField] private Renderer _dirt;
    [SerializeField] private Material _dryDirt;
    [SerializeField] private Material _wateredDirt;

    public string ID { get; private set; }

    private void Awake()
    {
        ID = transform.position.sqrMagnitude + "_" + transform.rotation.eulerAngles.sqrMagnitude;
        GameEvents.SaveInitiated += Save;
    }

    private void Start()
    {
        Load();

        //foreach (var item in _seeds)
        //{
        //    InventoryItem temp = new InventoryItem(item);
        //    _seedsItems.Add(temp);
        //}
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
    }

    private void CheckIfHasSeed()
    {
        if (_CurPlant == null)
        {
            //Debug.LogError("No Plant");
            if (InventorySystem._instance.CheckIfInventoryContains(_seeds[(int)_typeToPlant]))
            {
                //Debug.LogError(_seeds[(int)_typeToPlant].id);

                _plantButton.interactable = true;
            }
            else
                _plantButton.interactable = false;
        }
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
        InventorySystem._instance.Remove(_seeds[(int)_typeToPlant]);
        var temp =  Instantiate(Resources.Load("Crops/" + _typeToPlant.ToString()), _plantSpawnPoint.transform);
        _CurPlant = (GameObject)temp;
    }

    public void SetSeed(int seed)
    {
        _typeToPlant = (SeedType)seed;
        CheckIfHasSeed();   
    }

    public void ActvatePlantingUI()
    {
        _plantSelectUI.SetActive(true);
        PlayerController._instance.DisableControls();
        CheckIfHasSeed();
    }

    public void OnExit()
    {
        PlayerController._instance.EnableControls();
    }
}