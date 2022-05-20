using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Toggle _fullscreenToggle;

    private void Awake()
    {
        _fullscreenToggle.isOn = Screen.fullScreen;
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
