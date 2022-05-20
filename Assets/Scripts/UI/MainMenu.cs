using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private TMP_Dropdown _quality;

    private void Awake()
    {
        if (_fullscreenToggle != null)
            _fullscreenToggle.isOn = Screen.fullScreen;
        if (_quality != null)
        {
            _quality.value = PlayerPrefs.GetInt("_quality");
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("_quality"));
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

    public void Save()
    {
        GameEvents.SaveInitiated();
    }

    public void Load()
    {
        if (SaveSystem.SaveExists("Scene"))
            GameEvents.LoadInitiated();
    }
}
