using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private TMP_Dropdown _quality;
    [SerializeField] private Button _loadButton;
    [SerializeField] private GameObject _saveSuccesful;
    [SerializeField] private Slider _textSpeed;

    private void Awake()
    {
        if (_fullscreenToggle != null)
            _fullscreenToggle.isOn = Screen.fullScreen;
        if (_quality != null)
        {
            _quality.value = PlayerPrefs.GetInt("_quality");
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("_quality"));
        }
        if (_loadButton != null)
        {
            if (SaveSystem.SaveExists("Scene"))
                _loadButton.interactable = true;
        }
    }

    private void Start()
    {
        Gamemanager._instance.textSpeedMod = PlayerPrefs.GetFloat("_textSpeed");

        if (_textSpeed != null)
        {
            switch (PlayerPrefs.GetFloat("_textSpeed"))
            {
                case 0.5f:
                    _textSpeed.value = 1;
                    break;
                case 0.4f:
                    _textSpeed.value = 2;
                    break;
                case 0.3f:
                    _textSpeed.value = 3;
                    break;
                case 0.2f:
                    _textSpeed.value = 4;
                    break;
                case 0.1f:
                    _textSpeed.value = 5;
                    break;
                case 0.01f:
                    _textSpeed.value = 6;
                    break;
                default:
                    break;
            }
        }
    }

    public void LoadLevl(string level)
    {
        Gamemanager._instance.LoadLevel(level);
    }

    public void OpenTab(GameObject tab)
    {
        tab.SetActive(true);
    }

    public void CloseTab(GameObject tab)
    {
        tab.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FullScreen(bool input)
    {
        Screen.fullScreen = input;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("_quality", qualityIndex);
    }


    bool isSaving;
    public void Save()
    {
        if (!isSaving)
            StartCoroutine(SaveProcess());
    }

    IEnumerator SaveProcess()
    {
        isSaving = true;
        _saveSuccesful.SetActive(false);
        if (SaveSystem.SaveExists("SaveSuccessful"))
            SaveSystem.DeleteSingleFile("SaveSuccessful");
        yield return new WaitForEndOfFrame();
        SaveSystem.Save<int>(1, "SaveSuccessful");
        GameEvents.SaveInitiated();

        while (!SaveSystem.SaveExists("SaveSuccessful"))
        {
            yield return new WaitForSeconds(0.1f);
        }

        isSaving = false;
        _saveSuccesful.SetActive(true);
    }

    public void NewGame(string level)
    {
        Collection._instance.Clear();
        SaveSystem.SeriouslyDeleteAllSaveFiles();
        Gamemanager._instance.LoadLevel(level);
    }

    public void Load()
    {
        if (SaveSystem.SaveExists("Scene"))
            GameEvents.LoadInitiated();
    }

    public void EditName(string input)
    {
        Gamemanager._instance.Name = input;
        PlayerPrefs.SetString("Name", input);
    }

    public void EditSubPronouns(string input)
    {
        Gamemanager._instance.SubPronouns = input; 
        PlayerPrefs.SetString("SubPronouns", input);
    }

    public void EditObjPronouns(string input)
    {
        Gamemanager._instance.ObjPronouns = input;
        PlayerPrefs.SetString("ObjPronouns", input);
    }

    public void EditPosPronouns(string input) 
    { 
        Gamemanager._instance.PosPronouns = input;
        PlayerPrefs.SetString("PosPronouns", input);
    }

    public void EditRefPronouns(string input) 
    {
        Gamemanager._instance.RefPronouns = input;
        PlayerPrefs.SetString("RefPronouns", input);
    }

    public void EditBirthMonth(int input)
    {
        Gamemanager._instance.BirthMonth = input;
        PlayerPrefs.SetInt("BirthMonth", input);
    }

    public void EditBirthDay(int input)
    {
        Gamemanager._instance.BirthDay = input;
        PlayerPrefs.SetInt("BirthDay", input);
    }

    public void SetTextSpeed(float value)
    {
        switch (value)
        {
            case 1:
                Gamemanager._instance.textSpeedMod = 0.5f;
                PlayerPrefs.SetFloat("_textSpeed", 0.5f);
                break;
            case 2:
                Gamemanager._instance.textSpeedMod = 0.4f;
                PlayerPrefs.SetFloat("_textSpeed", 0.4f);
                break;
            case 3:
                Gamemanager._instance.textSpeedMod = 0.3f;
                PlayerPrefs.SetFloat("_textSpeed", 0.3f);
                break;
            case 4:
                Gamemanager._instance.textSpeedMod = 0.2f;
                PlayerPrefs.SetFloat("_textSpeed", 0.2f);
                break;
            case 5:
                Gamemanager._instance.textSpeedMod = 0.1f;
                PlayerPrefs.SetFloat("_textSpeed", 0.1f);
                break;
            case 6:
                Gamemanager._instance.textSpeedMod = 0.01f;
                PlayerPrefs.SetFloat("_textSpeed", 0.01f);
                break;
        }
    }
}
