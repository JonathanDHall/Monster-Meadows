using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}
