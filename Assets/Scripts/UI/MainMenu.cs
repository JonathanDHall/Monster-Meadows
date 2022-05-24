using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private TMP_Dropdown _quality;
    [SerializeField] private Button _loadButton;
    [SerializeField] private GameObject _saveSuccesful;

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

    public void LoadLevl(string level)
    {
        SceneManager.LoadScene(level);
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
        SceneManager.LoadScene(level);
    }

    public void Load()
    {
        if (SaveSystem.SaveExists("Scene"))
            GameEvents.LoadInitiated();
    }
}
